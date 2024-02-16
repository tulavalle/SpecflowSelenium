# BaseTestCore

<details>
<summary><b> PrepareExecution </b></summary>

Prepara a execução em relação aos diretórios:
- Padrão do projeto;
- Diretório de screenshots
- Diretório de arquivos
- inicializa o SettingsConfiguration

Exemplo de uso:

```c#
  BaseTestCore.PrepareExecution();
```

</details>

<details>
<summary><b> DriverInitialize </b></summary>

Realiza a inicialização do driver e aplica as opções desejadas para o navegador.
Realiza a inicialização do driver e aplica as opções desejadas para o navegador.

Exemplo de uso:

```c#
  BaseTestCore.DriverInitialize();
```

</details>

<details>
<summary><b> AccessPage </b></summary>

Realiza a inicialização do driver e aplica as opções desejadas para o navegador.

Exemplo de uso:

```c#
  BaseTestCore.AccessPage(driver, "https://url.com.br");

  //Se não informa a url será utilizada a url padrão indicada no appsettings do prpojeto de testes.
  BaseTestCore.AccessPage(driver);
```

</details>

<details>
<summary><b> MakeScreenshot </b></summary>

Realiza screenshot da tela.

Exemplo de uso:

```c#
  BaseTestCore.MakeScreenshot(driver, true, _setContext.CounterStep, _setContext.FilePath);
```

</details>

<details>
<summary><b> MakeScreenshotAndClosedDriver </b></summary>

Realiza screenshot e encerra o driver corrente.

Exemplo de uso:

```c#
  BaseTestCore.MakeScreenshotAndClosedDriver(driver, false, _setContext.CounterStep, _setContext.FilePath);
```

</details>

<details>
<summary><b> CloseDriver </b></summary>

Encerra o driver corrente.

Exemplo de uso:

```c#
 BaseTestCore.CloseDriver(driver);
```
</details>