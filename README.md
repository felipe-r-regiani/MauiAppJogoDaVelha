# AppJogoDaVelha

Jogo da Velha (Tic-Tac-Toe) desenvolvido em .NET MAUI. Dois jogadores alternam turnos clicando em células de um grid 3×3 para marcar X e O.

## Funcionalidades

- Grid 3×3 interativo para marcação de X e O
- Alternância automática de turnos entre jogadores
- Detecção de vitória (horizontal, vertical e diagonal)
- Detecção de empate (todas as células preenchidas sem vencedor)
- Botão "Novo jogo" para reiniciar a partida
- Desativação dos botões ao finalizar o jogo

## Como funciona

O app possui uma única tela (`MainPage`) com um `Grid` de 6 linhas × 3 colunas:

- **Linha 0**: Título "Jogo da Velha"
- **Linhas 1–3**: 9 botões (nomeados `btn10` a `btn32`) representando as células do tabuleiro
- **Linha 4**: Label de status ("Vez do X" / "Vez do O")
- **Linha 5**: Botão "Novo jogo" (inicialmente oculto)

Todos os 9 botões compartilham o mesmo evento `Button_Clicked`:

1. Identifica qual botão foi clicado via `sender`
2. Marca o botão com o símbolo do jogador atual (`X` ou `O`) e o desabilita
3. Atualiza a matriz lógica 3×3 lendo o texto de cada botão
4. Verifica vitória nas 3 direções (horizontal, vertical, diagonal)
5. Se vencedor: exibe "O jogador X/O venceu!" e finaliza
6. Se 9 jogadas sem vencedor: exibe "O jogo empatou"
7. Se o jogo continua: alterna a vez e atualiza o label de status

### Estrutura do Projeto

```
MainPage.xaml(.cs) → interface + lógica completa (code-behind, sem MVVM)
```

## Conceitos novos aprendidos

1. **Event handler compartilhado** — um único método `Button_Clicked` atende todos os 9 botões do tabuleiro, usando `sender` para identificar qual foi clicado. Evita criar 9 handlers separados.
2. **Mapeamento Grid → matriz lógica** — uso de `Grid.GetRow(btn) - 1` para converter linhas do Grid (1–3) em índices da matriz (0–2), já que a linha 0 é reservada ao título.
3. **Grid layout com RowDefinitions e ColumnDefinitions** — disposição de elementos em linhas e colunas proporcionais usando `*, *, *`.
4. **Matriz bidimensional (string[,])** — representação do estado lógico do jogo em uma matriz 3×3 para verificação de vitória.
5. **LinearGradientBrush** — fundo gradiente (roxo → vermelho) definido diretamente no XAML.
6. **Lógica de detecção de vitória** — conferência manual de todas as combinações vencedoras (3 horizontais, 3 verticais, 2 diagonais).
7. **Array.Clear()** — reset da matriz lógica ao iniciar novo jogo.
8. **foreach em Children do Grid** — iteração sobre todos os elementos visuais do Grid para resetar os botões.
