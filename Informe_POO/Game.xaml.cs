﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Informe_POO
{
    /// <summary>
    /// Lógica de interacción para Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        List<string> deckInp = new List<string>();
        List<string> dealerDeck = new List<string>();
        List<string> playerDeck = new List<string>();
        Card card = new Card();
        Dealer dealer = new Dealer();
        Player player = new Player();
        int playerScore = 0;
        int dealerScore = 0;
        public Game()
        {
            InitializeComponent();
        }
        private static int Check(List<string> cards)
        {
            int acumulado = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                string card = cards[i];
                char[] card1 = card.ToCharArray();
                if (card1.Count() != 3)
                {
                    string symbol = Char.ToString(cards[i][0]);
                    string suit = Char.ToString(cards[i][1]);
                    Card card2 = new Card(symbol, suit);
                    acumulado += card2.GetScore()[0];
                }
                else
                {
                    string symbol1 = "10";
                    string suit1 = Char.ToString(cards[i][2]);
                    Card card2 = new Card(symbol1, suit1);
                    acumulado += card2.GetScore()[0];
                }
            }
            return acumulado;
        }
        static int cont = 0;
        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            string playerName = Player.name;
            List<string> deck = dealer.Generate();
            List<string> deckInp = dealer.Randomize(deck);
            if (contPlayer != -1 || contDealer==0)
            {
                if (contPlayer == 0)
                {
                    playerDeck = player.Init(deckInp);
                    player.PlayerHand = playerDeck;
                    playerScore = Game.Check(playerDeck);
                    Dealer.Deck = deckInp;
                    Dealer.DealerHand = dealer.Init();
                    txtOutPut.Text += $"{playerName}, tus cartas son: {playerDeck[0]} {playerDeck[1]} \nTu acumulado es: {playerScore} \n\nLa primera carta del tallador es: {Dealer.DealerHand[0]}";
                    cont++;
                    contPlayer++;
                }
                else if (contPlayer==1)
                {
                    playerDeck = player.PlayerHand;
                    txtOutPut.Text = $"{playerName}, tus cartas son: ";
                    playerDeck.Add(dealer.Deal());
                    foreach (string element in playerDeck)
                    {
                        txtOutPut.Text += element + " ";
                    }
                    playerScore = Game.Check(playerDeck);
                    cont++;
                    txtOutPut.Text += $"\nTu acumulado es: {playerScore}\n\nLa primera carta del tallador es: {Dealer.DealerHand[0]}";
                    if (playerScore > 21)
                    {
                        MessageBox.Show("Haz perdido, tu acumulado ha sido mayor a 21. Suerte en una próxima oportunidad. Ahora es turno del tallador");
                        contPlayer = -1;
                        contDealer = 0;
                    }
                    else if (playerScore == 21 && cont == 2)
                    {
                        MessageBox.Show("Haz ganado automáticamente, haz obtenido 21. Felicitaciones!");
                        txtOutPut.Text += $"\n\n{playerName}, haz ganado. Felicitaciones!";
                        contPlayer = -1;
                    }
                    else if (playerScore == 21)
                    {
                        MessageBox.Show("Haz obtenido 21. Felicitaciones! Ahora es turno del tallador");
                        contPlayer = -1;
                        contDealer = 0;
                    }
                }
                if (contDealer == 0)
                {
                    dealerScore = Game.Check(Dealer.DealerHand);
                    txtOutPut.Text = $"Las cartas del tallador son: {Dealer.DealerHand[0]} {Dealer.DealerHand[1]} \nEl acumulado del tallador es: {dealerScore}";
                    if (dealerScore <= playerScore)
                    {
                        string esMenorIgual = "SI";
                        while (esMenorIgual=="SI")
                        {
                            if (playerScore > 21)
                            {
                                txtOutPut.Text += $"\n\n{playerName}, el tallador ha ganado este juego. Suerte en una próxima oportunidad";
                                esMenorIgual = "NO";
                            }
                            else if (playerScore == 21)
                            {
                                txtOutPut.Text = $"Las cartas del tallador son: ";
                                Dealer.DealerHand.Add(dealer.Deal());
                                foreach (string element in Dealer.DealerHand)
                                {
                                    txtOutPut.Text += element + " ";
                                }
                                dealerScore = Game.Check(Dealer.DealerHand);
                                txtOutPut.Text += $"\nEl acumulado del tallador es: {dealerScore}";
                                if (playerScore == dealerScore)
                                {
                                    txtOutPut.Text = $"Tanto el tallador como tú han obtenido el mismo acumulado, por lo que ninguno tuvo la fortuna de ganar. Suerte en una próxima oportunidad";
                                    esMenorIgual = "NO";
                                }
                                else if (playerScore<dealerScore)
                                {
                                    txtOutPut.Text += ($"\n\n{playerName}, haz ganado. Felicitaciones!");
                                    esMenorIgual = "NO";
                                }
                            }
                            else if (dealerScore > 21)
                            {
                                txtOutPut.Text += ($"\n\n{playerName}, haz ganado. Felicitaciones!");
                                esMenorIgual = "NO";
                            }
                            else if (dealerScore == 21)
                            {
                                txtOutPut.Text += $"\n\n{playerName}, el tallador ha ganado este juego. Suerte en una próxima oportunidad";
                                esMenorIgual = "NO";
                            }
                            else if (dealerScore > playerScore)
                            {
                                txtOutPut.Text += $"\n\n{playerName}, el tallador ha ganado este juego. Suerte en una próxima oportunidad";
                                esMenorIgual = "NO";
                            }
                            else
                            {
                                txtOutPut.Text = $"Las cartas del tallador son: ";
                                Dealer.DealerHand.Add(dealer.Deal());
                                foreach (string element in Dealer.DealerHand)
                                {
                                    txtOutPut.Text += element + " ";
                                }
                                dealerScore = Game.Check(Dealer.DealerHand);
                                txtOutPut.Text += $"\nEl acumulado del tallador es: {dealerScore}";
                            }
                        }
                    }
                    else
                    {
                        txtOutPut.Text += $"\n\n{playerName}, el tallador ha ganado este juego. Suerte en una próxima oportunidad";
                    }
                }
            }
            else
            {
                MessageBox.Show("El juega ya ha terminado, si quieres jugar de nuevo solo es cuentión de oprimir el botón \"Jugar de nuevo\"");
            }
        }
        private static int contPlayer = 0;
        private static int contDealer = -1;

        private void btnPlant_Click(object sender, RoutedEventArgs e)
        {
            string playerName = Player.name;
            if (contPlayer == 1)
            {

                if (MessageBox.Show("¿Estas seguro de que quieres plantar? Una vez plantes, tu turno habrá llegado a su fin, por lo que la próxima vez que oprimas el botón \"Solicitar\" empezará el turno del tallador", "Mensaje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    contPlayer = -1;
                    contDealer = 0;
                }
                else
                {
                    contPlayer = 1;
                    contDealer = -1;
                }
            }
            else
            {
                MessageBox.Show("El juega ya ha terminado, si quieres jugar de nuevo solo es cueestión de oprimir el botón \"Jugar de nuevo\"");
            }
        }
    }
}