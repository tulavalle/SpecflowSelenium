# Wait

<details>
<summary><b> WaitFor </b></summary>

Realiza uma espera expl�cita, ou seja, com tempo fixo.

Exemplo de uso:

```c#
  WaitsCore.WaitFor(1000);
```

</details>


<details>
<summary><b> WaitForElementCondition </b></summary>

Efetua uma espera impl�cita (at� que a condi��o esperada pelo elemento seja atendida, caso contr�rio, a espera persistir� at� atingir o timeout informado).

Exemplo de uso:

```c#
  WaitsCore.WaitForElementCondition(driver, "[id='teste']", WaitsCondition.NotExist, 20);

  //Se n�o informar o timeout ser� considerado o default (10)
  WaitsCore.WaitForElementCondition(driver, "[id='teste']", WaitsCondition.NotExist);
```

</details>

<details>
<summary><b> WaitForAmountOfOpenTabs </b></summary>

Pesquise o n�mero de guias e muda o foco para a guia especificada.

Exemplo de uso:

```c#
  WaitsCore.WaitForAmountOfOpenTabs(driver, 3, 20);

  //Se n�o informar o timeout ser� considerado o default (10)
  WaitsCore.WaitForAmountOfOpenTabs(driver, 3);
```

</details>

<details>
<summary><b> AguardarOCarregamentoDaPagina </b></summary>

Aguarda que a p�gina seja carregada por "document.readyState" (JavaScript).

Exemplo de uso:

```c#
  WaitsCore.AguardarOCarregamentoDaPagina(driver);
```

</details>

<details>
<summary><b> WaitAlert </b></summary>

Aguarda o Alerta estar presente.

Exemplo de uso:

```c#
  WaitsCore.WaitAlert(driver, 30);

  //Se n�o informar o timeout ser� considerado o default (10)
  WaitsCore.WaitAlert(driver);
```

</details>

<details>
<summary><b> WaitForTime </b></summary>

Aguardar pelo tempo m�ximo informado.

Exemplo de uso:

```c#
  WaitsCore.WaitForTime(driver, Waits.FromMinutes, 10);

  //Se n�o informar o timeout ser� considerado o default (10)
  WaitsCore.WaitForTime(driver, Waits.FromMinutes);
```

</details>

<details>
<summary><b> WaitCondition </b></summary>

Aguarda a condi��o Selenium ser atendida, pelo tempo m�ximo definido.

Exemplo de uso:

```c#
  WaitsCore.WaitCondition(driver, By.CssSelector("[is='descricaoDoId']"), SeleniumConditions.BeClickableElement, 20);
  
  //Se n�o informar o timeout ser� considerado o default (10)
  WaitsCore.WaitCondition(driver, By.CssSelector("[is='descricaoDoId']"), SeleniumConditions.BeClickableElement);
```

</details>

<details>
<summary><b> WaitCondition </b></summary>

Persiste a espera pelo elemento at� o tempo definido.

Exemplo de uso:

```c#
  WaitsCore.WaitCondition(driver, By.CssSelector("[is='descricaoDoId']"), Waits.FromMinutes, 20);
  
  //Se n�o informar o timeout ser� considerado o default (10)
  WaitsCore.WaitCondition(driver, By.CssSelector("[is='descricaoDoId']"), Waits.FromMinutes);
```

</details>

<details>
<summary><b> WaitCondition </b></summary>

Persiste conforme a condi��o Selenium paro elemento do localizador com o texto informado

Exemplo de uso:

```c#
  WaitsCore.WaitCondition(driver, By.CssSelector("[is='descricaoDoId']"), SeleniumConditions.BeClickableElement, "Salvar");
```

</details>

