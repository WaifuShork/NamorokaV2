using System;
using System.Threading.Tasks;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2.NamorokaCore.Modules
{
    public class CompoundInterestModule : InteractiveBase
    {
        [Command("compound", RunMode = RunMode.Async)]
        public async Task CompoundInterestAsync()
        {
            double p;
            double r;
            double n;
            double t;
            double m;
            SocketMessage principle;
            SocketMessage rate;
            SocketMessage nt;
            SocketMessage time;
            SocketMessage months;
            
            do
            {
                await ReplyAsync("Principle: ");
                principle = await NextMessageAsync();
            } 
            while (!double.TryParse(principle.ToString(), out p));

            do
            {
                await ReplyAsync("Annual nominal interest: ");
                rate = await NextMessageAsync();
            } 
            while (!double.TryParse(rate.ToString(), out r));

            do
            {
                await ReplyAsync("Times it compounds per year: ");
                nt = await NextMessageAsync();
            } 
            while (!double.TryParse(nt.ToString(), out n));

            do
            {
                await ReplyAsync("Time in years: ");
                time = await NextMessageAsync();
            } 
            while (!double.TryParse(time.ToString(), out t));

            do
            {
                await ReplyAsync("Monthly contributions: ");
                months = await NextMessageAsync();
            } 
            while (!double.TryParse(months.ToString(), out m));
            
            var a = p * Math.Pow((1 + r / n), (n * t));

            var f = m * (Math.Pow((1 + r / n), n * t) - 1) / (r / n);

            var sumTotal = a + f;

            var finalTotal = Math.Round(sumTotal, 2);
            await ReplyAsync($"Final amount is {finalTotal}kr");
        }
    }
}