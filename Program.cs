using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.IO;

namespace Bot_V4
{
    public class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult(); //defines a new method that is async


        private DiscordSocketClient _client; //actual discord bot = client

        public async Task MainAsync() //creates client
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;

            _client.Log += Log;


            var token = "insert your token here";


            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            //prevents program from closing before task is compelted
            await Task.Delay(-1);

        }


        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        //command handler
        //recieves mesages in discord
        private Task CommandHandler(SocketMessage message)
        {

            string command = "";
            int lengthOfCommand = -1;


            //filtering message begins here //prefix
            if(!message.Content.StartsWith('!')) //if doesnt start with ! then return completed
                return Task.CompletedTask;

            if(message.Author.IsBot)    //this ignores all commands from bots
                return Task.CompletedTask;


            if (message.Content.Contains(' '))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;


            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower(); //brings everything to lower case


            //commands begin here
            if (command.Equals("hello"))
                message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}"); //sends message from bot to discord

            else if(command.Equals("age"))
            {
                message.Channel.SendMessageAsync($@"Your account was created at {message.Author.CreatedAt.DateTime.Date}");
            }


            return Task.CompletedTask;
        }


    }
}
