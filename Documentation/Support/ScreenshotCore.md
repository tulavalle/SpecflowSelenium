 # ScreenshptCore

<details>
<summary><b> MakeScreenshot </b></summary>

Realiza o screenshot e salva no diretório desejado.

Exemplo de uso:

```c#

 BaseTestCore.MakeScreenshot(driver, _setContext.CounterStep, _screenshotsFolder, browser, true, counter, _filePath, _fileName);
```

</details>

<details>
<summary><b> ReturnFilePathFormatted </b></summary>

Monta o caminho completo para salvar o arquivo.

Exemplo de uso:

```c#

var filePathFormatted = ScreenshotCore.ReturnFilePathFormatted(ScenarioContext, ScreenshotDirectory, filename, Browser, isStep, stepCounter);

```

</details>

<details>
<summary><b> ReturnScenarioName </b></summary>

Monta o caminho completo para salvar o arquivo.

Exemplo de uso:

```c#

var filePathFormatted = ScreenshotCore.ReturnFilePathFormatted(ScenarioContext, ScreenshotDirectory, filename, Browser, isStep, stepCounter);

```

</details>

<details>
<summary><b> Busca a descrição da Tag que inicia com TC_. </b></summary>

Busca a descrição da Tag que inicia com TC_.

Exemplo de uso:

```c#

var fileName = ScreenshotCore.ReturnScenarioNameByTagTestCase(ScenarioContext)

```

</details>

<details>
<summary><b> ReturnIfContainsTagTestCase </b></summary>

Busca a lista de tags e verifica se contém a tag do Test Case (TC_)

Exemplo de uso:

```c#

var containsTagTC = ScreenshotCore.ReturnIfContainsTagTestCase(ScenarioContext)

```

</details>