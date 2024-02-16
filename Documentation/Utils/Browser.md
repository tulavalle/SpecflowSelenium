# Browser

<details>
<summary><b> Refresh </b></summary>

Executa atualiza��o da p�gina.

Exemplo de uso:

```c#
  Browser.Refresh(driver);
```

</details>

<details>
<summary><b> OpenNewTab </b></summary>

Abre uma nova aba na janela do navegador.

Exemplo de uso:

```c#
  Browser.OpenNewTab(driver);
```

</details>

<details>
<summary><b> OpenNewTab </b></summary>

Abre uma nova janela do navegador.

Exemplo de uso:

```c#
  Browser.OpenNewWindow(driver);
```

</details>

<details>
<summary><b> ChangeToTabCurrent </b></summary>

Muda o foco do driver para a janela corrente.

Exemplo de uso:

```c#
  Browser.ChangeToTabCurrent(driver);
```

</details>

<details>
<summary><b> ChangeToTabCurrent </b></summary>

Muda o foco do driver para a janela corrente.

Exemplo de uso:

```c#
  Browser.ChangeToTabCurrent(driver);
```

</details>

<details>
<summary><b> NavigateToUrl </b></summary>

Navega para a URL desejada.

Exemplo de uso:

```c#
  Browser.NavigateToUrl(driver, "https://google.com");
```

</details>

<details>
<summary><b> GetCurrentURL </b></summary>

Obt�m a URL da p�gina atual.

Exemplo de uso:

```c#
  Browser.GetCurrentURL(driver);
```

</details>

<details>
<summary><b> SwitchTab </b></summary>

Obt�m o n�mero de abas e muda para a aba desejada.

Exemplo de uso:

```c#
  Browser.SwitchTab(driver, 3);
```

</details>

<details>
<summary><b> ClickBrowserAlertButton </b></summary>

Muda o foco do driver para o alerta realiza a a��o desejada (confirmar ou cancelar) para o alerta.

Exemplo de uso:

```c#
  Browser.ClickBrowserAlertButton(driver, true, false);
  Browser.ClickBrowserAlertButton(driver, false, true);
  Browser.ClickBrowserAlertButton(driver);
```

</details>

<details>
<summary><b> SetValueAlert </b></summary>

Muda o foco do driver para o alerta Busca a mensagem exibida no mesmo.

Exemplo de uso:

```c#
  var messageFound = Browser.ClickBrowserAlertButton(driver, true, false);
  var messageFound = Browser.ClickBrowserAlertButton(driver, false, true);
  var messageFound = Browser.ClickBrowserAlertButton(driver);
```

<details>
<summary><b> SetValueAlert </b></summary>

Muda o foco do driver para o alerta e insere o o valor desejado no input do alert.

Exemplo de uso:

```c#
  Browser.SetValueAlert(driver, "texto desejado");
```

</details>
 
<details>
<summary><b> MoveToIFrame </b></summary>

Move a inst�ncia do driver para o iframe.

Exemplo de uso:

```c#
  Browser.MoveToIFrame(driver, "texto desejado");
  //Se n�o for informado a tag desejada � utilizada a padr�o "iframe"
  Browser.MoveToIFrame(driver);
```

</details>

<details>
<summary><b> LeavingAIFrame </b></summary>

Move a inst�ncia do driver do o iframe para o driver default.

Exemplo de uso:

```c#
  Browser.LeavingAIFrame(driver);  
```

</details>