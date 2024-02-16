using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace SpecflowSelenium.Support
{
    public partial class S3BucketAmazon
    {
        private static IAmazonS3 s3Client;

        private string accessKey;
        private string secretKey;
        private string region;
        private static string bucketName;
        private string fileNameInS3;
        private string filePath;

        private static void Initialize(string accessKey, string secretKey, string region)
        {
            s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

            try
            {
                s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }

        /// <summary>
        /// Método que inicializa o Client, se estiver nulo e realiza o upload da imagen ma pasta desejada (local ou S3 Bucket)
        /// </summary>
        /// <param name="scenarioContext">Contexto do etste corrente</param>
        /// <param name="filePathLocal">Diretório padrão de screenshot local</param>
        /// <param name="fileName">Nome do Arquivo conforme local a ser salvo (local ou S3 Bucket)</param>
        /// <exception cref="Exception">Retorna erro ao realizar o upload</exception>
        public async Task UploadFile(ScenarioContext scenarioContext, string filePathLocal, string fileName)
        {
            GlobalVariables.Configuration = SettingsConfiguration.Configuration;

            accessKey = GlobalVariables.Configuration["appsettings:s3Aws:AWSAccessKey"];
            secretKey = GlobalVariables.Configuration["appsettings:s3Aws:AWSSecretKey"];
            region = GlobalVariables.Configuration["appsettings:s3Aws:AWSRegion"];
            bucketName = GlobalVariables.Configuration["appsettings:s3Aws:AWSBucket"];
            fileNameInS3 = fileName;
            filePath = filePathLocal;

            var keyPath = FormatStrings.FormatPath(fileNameInS3);
            scenarioContext.Set(keyPath, "fileNameInS3");

            if (s3Client == null)
                Initialize(accessKey, secretKey, region);

            try
            {
                PutObjectRequest putRequest = new()
                {
                    BucketName = bucketName,
                    Key = FormatStrings.FormatPath(keyPath),
                    FilePath = filePath
                };

                await s3Client.PutObjectAsync(putRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw new Exception("Error occurred: " + amazonS3Exception.Message);
            }
        }
    }
}