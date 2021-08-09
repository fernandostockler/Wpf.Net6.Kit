# **SideMenuItem**
Representa um ítem do menu de items do controle SideMenu. SideMenuItem deriva de ListBoxItem e acrescenta algumas propriedades para ajustar o icone, o indicador de selecionado entre outras.

## Propriedades de navegação

Propriedade | Type | Descrição
----- | ------ | -----
**`PageTypeName`** | string | Obtem ou define a propriedade PageTypeName que representa a chave no dicionário da propriedade Pages do controle SideMenu associado, responsável pela navegação.

<br/>

## Propriedades do icone

Propriedade | Type | Descrição
----- | ------ | -----
**`Symbol`** | string |  Obtem ou define uma cadeia de caracteres que representa o código do símbolo exibido como o icone.
**`SymbolFontFamily`** | FontFamily | Obtem ou define o FontFamily utilizado pela propridade Symbol.
**`SymbolForeground`** | Brush | Obtem ou define o pincel que representa a cor do icone.
**`SymbolFontSize`** | double | Obtem ou define o tamanho da fonte responsável pelo tamanho icone.
**`SymbolMargin`** | Thicness | Obtem ou define a margem em torno do icone.

<br/>

Exemplos:

~~~~
<controls:SideMenuItem Content="Page2" PageTypeName="Page2" Symbol="&#xE136;" SymbolForeground="MediumSlateBlue"/>
~~~~
Resulta em:

![SideMenuItem sample](/Wpf.Net6.Kit/Docs/Assets/SideMenuItem/SideMenuItem_samplePage2.png?raw=true)

## Propriedades do indicador de seleção

Propriedade | Type | Descrição
---- | ---- | -----
**`SelectionIndicatorBrush`** | Brush | Obtem ou define o pincel que representa a cor do indicador de seleção.
**`SelectionIndicatorWidth`** | double | Obtem ou define a largura do indicador de seleção.
**`SelectionIndicatorMargin`** | Thickness | Obtem ou define a margem em torno do indicador de seleção.
