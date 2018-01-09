using Discord.Commands;
using Discord;
using Discord.Net;
using Discord.Webhook;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PPExDBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("Help")]
        public async Task HelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("!FakkePPE @Name", "Says if someone is a fake PPE", false)
                .AddField("!GiveRole @Name Role", "Assings someone a role, only if you have permission", false)
                .AddField("!Tableflip", "Just try it", false)
                .AddField("!Kick @Name", "Kicks Someone, Only with permission", false)
                .AddField("!Meme", "What do you think this does?")
                .AddField("? Prefix", "If you type ? before a message it gets an upvote and a downvote");
            await ReplyAsync("", false, builder.Build());
        }
        [Command("supersecretppecommandxdlmao")]
        public async Task XDASYNC()
        {
            var user = Context.User.Username;
            var guild = Context.Guild;
            var channel = guild.DefaultChannel;
            string xd = ("HOLY BALLS " + user + " TRIGGERED THE SPECIAL COMMAND");
            await channel.SendMessageAsync(xd);
        }

        [Command("FakePPE")]
        public async Task PingAsync([Remainder] string name = "Dylan")
        {


            //Context.User;
            //Context.Client;
            //Context.Guild;
            //Context.Message;
            //Context.Channel
            await ReplyAsync($"{name} is a fake ppe");
        }
        [Command("GiveRole")]
        public async Task RoleAsync(IGuildUser user, [Remainder] string roleName)
        {
            var CommandCaller = Context.User as IGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == roleName);
            if (CommandCaller.GuildPermissions.ManageRoles == true)
            {

                await (user as IGuildUser).AddRoleAsync(role);
            }
        }
        [Command("TableFlip")]
        public async Task FlipAsync()
        {
            await ReplyAsync(" ┬─┬ ノ( ゜-゜ノ)");
            await Task.Delay(2000);
            await ReplyAsync(" (╯°□°）╯︵ ┻━┻");

        }
        [Command("Kick"), RequireUserPermission(GuildPermission.BanMembers)]
        public async Task KickAsync(IGuildUser user)
        {
            await (user as IGuildUser).KickAsync();
        }
        [Command("Meme")]
        public async Task MemeSync()
        {
            await Context.Channel.SendFileAsync("meme/xd1.jpg");
            await Context.User.SendMessageAsync("You Filthy Memer");
        }
        [Command("Max")]
        public async Task MaxSync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("Oryxshop.net", "DeCaS aRe iN sToCk nOw!");
            await ReplyAsync("",false, builder.Build());
            
        }


    }
}
