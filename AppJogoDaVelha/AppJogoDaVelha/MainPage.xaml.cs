namespace AppJogoDaVelha
{
    public partial class MainPage : ContentPage
    {
        // Variáveis para controlar o estado do jogo
        string vez = "X";
        int jogadas = 0;
        bool jogo_finalizado = false;

        string[,] matriz_jogadas = new string[3, 3];
        //[0, 0][0, 1][0, 2]
        //[1, 0][1, 1][1, 2]
        //[2, 0][2, 1][2, 2]

        public MainPage()
        {
            InitializeComponent();
        }

        public async void ResetarJogo(object sender, EventArgs e)
        {
            //Reseta os valores
            vez = "X";
            jogadas = 0;
            jogo_finalizado = false;
            text_recado.Text = $"Vez do {vez}";
            Array.Clear(matriz_jogadas, 0, matriz_jogadas.Length);

            foreach (var botao in jogo.Children) // Reseta os botões
            {
                if(botao is Button btn)
                {
                    btn.Text = "";
                    btn.IsEnabled = true;
                }
            }

            button_resetar_jogo.Text = "Novo jogo";
            button_resetar_jogo.IsVisible = false;

        }

        public void FimDeJogo()
        {
            jogo_finalizado = true;

            foreach (var botao in jogo.Children)
            {
                if (botao is Button btn)
                {
                    btn.IsEnabled = false;
                }
            }

            button_resetar_jogo.IsEnabled = true;
        }

        public void AtualizarMatriz()
        {
            foreach (var botao in jogo.Children)
            {
                if (botao is Button btn)
                {
                    int linha = Grid.GetRow(btn) - 1;
                    int coluna = Grid.GetColumn(btn);

                    if (btn.Text == "X")
                    {
                        matriz_jogadas[linha, coluna] = "X";
                    }
                    else if (btn.Text == "O")
                    {
                        matriz_jogadas[linha, coluna] = "O";
                    }
                }
            }
        }
    
        public Boolean ConferirHorizontal()
        {
            for (int i = 0; i < 3; i++)
            {
                if (matriz_jogadas[i, 0] != null &&
                    matriz_jogadas[i, 0] == matriz_jogadas[i, 1] &&
                    matriz_jogadas[i, 0] == matriz_jogadas[i, 2]) 
                    { 
                        return true; 
                    }
            }

            return false;
        }

        public Boolean ConferirVertical()
        {
            for (int i = 0; i < 3; i++)
            {
                if (matriz_jogadas[0, i] != null &&
                    matriz_jogadas[0, i] == matriz_jogadas[1, i] &&
                    matriz_jogadas[0, i] == matriz_jogadas[2, i])
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean ConferirDiagonal()
        {
            if (matriz_jogadas[0, 0] != null &&
                matriz_jogadas[0, 0] == matriz_jogadas[1, 1] &&
                matriz_jogadas[0, 0] == matriz_jogadas[2, 2])
            {
                return true;
            }

            if (matriz_jogadas[0, 2] != null &&
                 matriz_jogadas[0, 2] == matriz_jogadas[1, 1] &&
                 matriz_jogadas[0, 2] == matriz_jogadas[2, 0])
            {
                return true;
            }

            return false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button clicado = (Button)sender;
            
            jogadas +=1; // Incrementa o número de jogadas
            clicado.Text = vez; // Altera o texto do botão
            clicado.IsEnabled = false; // Impede que o botão seja clicado novamente
            button_resetar_jogo.IsVisible = true; // Exibe o botão de resetar
            AtualizarMatriz(); // Atualiza a matriz de jogadas com os valores atuais dos botões
            Console.WriteLine(matriz_jogadas);

            // DisplayAlertAsync("teste", clicado.Text + " " + vez, "ok");

            // Conferindo se houve vencedor depois de cada jogada
            if (ConferirHorizontal() || ConferirVertical() || ConferirDiagonal())
            {
                FimDeJogo();
                text_recado.Text = $"O jogador {vez} venceu!";
            }

            // Caso as 9 jogadas sejam feitas e ninguém tenha ganhado, é um empate
            if (jogadas == 9)
            {
                FimDeJogo();
                text_recado.Text = "O jogo empatou";
            }

            // Se o jogo ainda nao finalizou, troca a vez do jogador
            if (!jogo_finalizado)
            {
                vez = (vez == "X") ? "O" : "X";
                text_recado.Text = $"Vez do {vez}";
            }

        }
    }
}
