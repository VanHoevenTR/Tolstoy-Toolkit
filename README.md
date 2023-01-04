# Tolstoy Toolkit by VanHoevenTR

## Important

This is useless now because the developer of Tolstoy Comments patched everything to combat bots, spam and pro-ukrainians.

RT (Russia Today) will now be able to ban your accounts even through you only vote comments and the account was created from other website. This was not a thing before

See more https://blogs.tolstoycomments.com/glavnyy-apdeyt-yetogo-goda-novye-instr/#more-1426

## Introduction

![](https://i.imgur.com/vHB4UBI.png)

I decided to create my own bots written in C# because there are downvoter bots going on, kept mass downvoting pro-ukrainian comments, even some pro-russian (pro-ukrainian who pretended to be pro-russian maybe?). According to some users and some research, it was Z__Z__Z who was all behind of it [(PROOF)](https://imgur.com/a/klDCUbm). I wrote this tool in rush, so the code might be messy, and also I have less experience in C#, my friend usually help me with this, but not anymore.

Because I do not know how to bypass invisible captcha, I use tokens. It can be easly obtained from the browser after logged in (Explained below). It takes time but you will build up many bots slowly

Always be prepared with vote fighting. 

[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua) [![Russian Warship Go Fuck Yourself](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/RussianWarship.svg)](https://stand-with-ukraine.pp.ua) [![Made in Ukraine](https://img.shields.io/badge/made_in-ukraine-ffd700.svg?labelColor=0057b7)](https://stand-with-ukraine.pp.ua)

## Features
- Comment voting
   - Tabs (Currently not possible to save session)
   - Max voting
   - Parallel voting
   - Delay voting
   - Downvote rating in between
   - Upvote rating in between
   - Export to CSV
   - Load history. View voted comments locally. Comment will be highlighted green if upvoted, and red if downvoted
   - Dynamic loading comment, rating and user info when selecting a comment
   - Load comments from article url or user ID
   - Mass voting comment(s)
   - Support rt.com, russian.rt.com and arabic.rt.com (actualidad.rt.com is using Disqus commenting system by US. I will NOT SUPPORT Disqus. Instead, please ask Disqus to revoke RT from using it.)
- Tokens
   - Edit tokens.txt directly
   - Open tokens.txt via external editor
   - Get token user info
   - Export to CSV
- User info
   - Edit user list
   - Auto save
   - Auto reload
   - Load RT admins and mods. Note: Their roles are default after we exposed them but some of them are still active
   - Load Z___Z___Z bots. Allows you to see Z___Z___Z's activity. Damn, he have so many bots lol
   - Export to CSV
- Messenger (Token with verified email is required)
   - Friends list
- Miscellaneous (I made it to myself bt feel free to try it)
   - Brute force user info
   - Mass send message
- Options
   - Update check

## TODO (Help wanted)

- Websocket and how to inspect websocket traffic: Needed to manipulate "Now writing"
- Faster way to request web GET and POST
- Bypass invisiable captcha? I don't know if it's possible

## How to use the tool

You need a computer running Windows 7 and above that supports NET framework 4.8 (Win10 and above comes with .NET preinstalled)

To run .exe directly on Linux or macOS, you can try use [Wine](https://www.winehq.org/), [Bottle](https://usebottles.com/), [PlayonLinux](https://www.playonlinux.com/en/) or [CrossOver (Paid)](https://www.codeweavers.com/). I haven't tried these, since I don't use Linux or macOS.

This tool is really simple to use

Download the tool: https://mega.nz/folder/PUJ12T6Z#0MrSFBO8QTMGqMDGdNxk5g

WARNING: Tolstoy Toolkit sometimes get flagged as malicious by security software. Of course, Tolstoy Toolkit is safe. If you have some programming skills, you can even verify that yourself by looking at the source code or check exe using dnSpy. However, some anti-malware tools are over-sensitive and flag programs as malicious if they are not sure. The easiest way of fixing the problem is to send the file to the developer of your security software

Run it

Read below how to obtain token

Add your tokens within the tool

When you are done, you can start voting

Copy and paste RT article url or User ID into the text box (Supported sites rt.com, russian.rt.com and arabic.rt.com), and load it

Double click to view profile comments. Hold SHIFT or CTRL to select multiple comments. CTRL + A to select all

Have fun voting :D

Do not forget, Tolstoy have rate limits, don't aggressively voting. Try to limit voting or delay to keep your tokens safe from rate limit. ~~Be aware Z__Z__Z bots may be quick enough to detect upvoted comments and re-downvote comments again, so be prepared to vote fast.~~ Rate limit starts with 5 mins, if you keep hitting limits, you token(s) will be limited for up to 24 hours or so

## How to obtain token

~~**Important: Do not create account directly on RT. RT admins/mods can see new accounts including email and IP tied to it, they will permanent ban if the account looks suspecious. You MUST use my website to create accounts to bypass ban and get tokens because only me can see your account (can't see anyway after trial version expired). Account(s) will only be listed on RT moderation system once you begin to comment on RT, they can't see if you only voting comments**~~

~~**If you really want to create accounts directly on RT, use appropriate name and email, and verify email. Create account slowly. You can use your RT accounts that are not banned yet**~~

Ok, let's get started

Go to my website https://vanhoeven.bitbucket.io/#tc-profile

Create an account. Write scrambled email, username and password. No need to verify email

![](https://i.imgur.com/2hkJDTh.png)

Chrome/Chromium based browsers: Right click anywhere, click **Inspect**, Go to **Application**, **Local Storage**, **https://web.tolstoycomments.com** and copy your `token`. If the `token` does not refresh or missing, simply click on circle arrow icon

![](https://i.imgur.com/rX4ENZq.png)

Firefox/Firefox based browsers: Right click anywhere, click **Inspect (Q)**, Go to **Storage**, **Local Storage**, **https://web.tolstoycomments.com** and copy your `token`

![](https://i.imgur.com/lZ2QisD.png)

Open the tool, click **Tokens** tab, and paste the token. Hit enter to create a new line ready for another token. It is recommended to edit tokens.txt using advanced text editor such as Notepad++ with line numbers

![](https://i.imgur.com/LwbOTlL.png)

Log out, it will not make token invalid (Which is good)

Rinse and repeat. Create many accounts as you can. I think you can use macro like https://www.autohotkey.com/ but i'm not familar with macros

## Development

Use Visual Studio 2022 to compile the project

For HTTP debugging, I used Fiddler Classic https://www.telerik.com/download/fiddler. You can also do that via browser's inspector tool

## Contact

Discord: `VanHoeven#5150`

Email: vanhoeventr@proton.me

_No, I'm not Dutch lol_
