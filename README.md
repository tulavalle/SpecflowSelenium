# __SpecflowSelenium__

## _Core de Automação e2e com Selenium 4+_

O projeto SpecfloeSelenium é um projeto class library para geração de pacote Nuget com as implementações básicas para execução de testes e2e com selenium.

## Arquitetura

- Projeto Net7.0
- Selenium 4+
- Specflow

## Instalação

- Fazer o [Download da SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) com a versão correspondente a versão do netcore do projeto (6.0.x), conforme o sistema operacional.
  
- Após instalar a SDK, é necessário confiar no certificado que foi instalado pelo sdk através do comando:
- ``` dotnet dev-certs https --trust ```

## DSL

### _Driver_

Classes responsáveis pelo controle o driver do selenium para a execução dos testes.

- Driver Factory: Responsável por obter o Webdriver do Selenium ou o RemoteWebDriver do Selenium Grid para o navegador desejado e responsável pelas finalizações doo driver.

- DriverOptionsFactory: Responsável por obter as configurações de opções para o navegador desejado.

### _FindsByFactory_

Customização do PageFactory do Selenium para trabalhar com componentização.

- FindsElementsBy: Responsável pelos enumeradores que definem as opções de seletores css a serem utilizados para a localização dos componentes da tela.

- PageFactoryElements: Responsável pela inicialização dos componentes da tela.

### _Support_

Itens de apoio a implementação de itens das demais pastas.

- Enumerator: Responsável por retornar a descrição de um enumerador.

- ExceptionFactory: Responsável por retornar uma exceção informando que o teste está desatualizado, pendente ou possui bug reportado.

- GlobalVariables: Responsável por inicializar as variáveis globais para Driver e Timeout.

- Screenshot: Reponsável fazer um screenshot no momento da execução do teste e salvar um arquivo .png na pasta padrão com o nome do cenário atual. Para o LivingDoc na Azure tem posssibilidade de salvar no Bucket da Amazon.
- 

### _Utils_

Classes com métodos Selenium úteis e recorrentes em relação a implementação de testes.

- Browser: Responsável pelas ações relacionadas ao navegador.

- FileActions: Reponsável pela lógica para os testes poderem utilizar comandos JS.

- JavaScript: Reponsável pela lógica para os testes poderem utilizar comandos JS.

- SeleniumCommands: Realiza a ações do Selenium.
  
- Wait: Responsáveis por esperas conforme condição em JavaScript e Esperas pelas condições do Selenium.

## Regras para versionamento do pacote

### _Padrão de versionamento_

O versionamento deve seguir o padrão: `Principal.Secundário.Path[-Sufixo]`, sendo:

>- `Principal`: alterações recentes
>- `Secundário`: novos recursos, mas compatível com versões anteriores
>- `Patch`: somente correções de bugs compatíveis com versões anteriores
>- `-Sufixo` (opcional): um hífen seguido por uma cadeia de caracteres denotando uma versão de pré-lançamento.
Mais informações consulte a convenção [Controle de Versão Semântico](https://semver.org/spec/v1.0.0.html)

### _Versões de pré-lançamento_

É definida pelo "-sufixo" indicado na versão do pacote, as convenções de nomenclatura reconhecidas são:

>- `-alpha`: Versão Alfa, normalmente usada para trabalho em andamento e experimentação.
>- `-beta`: versão beta, normalmente uma completa com recursos para a próxima versão planejada, mas pode conter erros conhecidos.
>- `-rc`: versão Release candidate, normalmente uma versão que é potencialmente a final (estável), a menos que surjam bugs significativos.

__Exemplos__:

```text
1.0.1
6.11.1231
4.3.1-rc
2.2.44-beta1
```

Mais detalhes sobre versionamento [aqui](https://docs.microsoft.com/pt-br/nuget/concepts/package-versioning).

## Comandos úteis

- Restaura os pacotes utilizados no projeto:
  ```dotnet restore```

- Realiza o build do projeto (e todas as suas dependências):
  ```dotnet build```

- Empacota o projeto:
```dotnet pack --configuration Release```
