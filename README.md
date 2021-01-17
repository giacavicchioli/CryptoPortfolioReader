# CryptoPortfolioReader
Get unified crypto portfolio assets from your accounts in Telegram

## Supported Crypto Exchange:

- [Coinbase Pro](https://pro.coinbase.com/)
- [Binance](https://www.binance.com/)
- More coming soon..

## Exchanges Setup

You need to create you account api keys from the official website (when available give the keys only read privileges) and paste in config.json file, in the following section:

    "portfolios": [
        {
            "name": "CoinbasePro",
            "config": {
                "apiKey": "COINBASE_PRO_API_KEY",
                "apiPassphrase": "COINBASE_PRO_PASSPHRASE",
                "apiSecret": "COINBASE_PRO_SECRET"
            }
        },
        {
            "name": "Binance",
            "config": {
                "apiKey": "BINANCE_API_KEY",
                "apiSecret": "BINANCE_API_SECRET"
            }
        }
    ],

All the exchanges are optional.

More exchanges coming soon, **PR are welcome!**

## Telegram Setup

Telegram notification service was inspired by this tutorial [link](https://tuledev.github.io/logger/swift/telegram-bot-as-a-real-time-logger/)

### Create Telegram bot, and get bot token
- Search BotFather in Telegram.
- Create new bot with command /newbot.
- Choose a name for your bot, after that you can get the BOT_TOKEN of the bot to access the HTTP API.
- Open your bot, press Start

### Create Telegram group, and get chat ID

- Create a Telegram group, add the bot to the group.
- Get the chat group id by paste this to a browser https://api.telegram.org/bot[BOT_TOKEN]/getUpdates

(If you dont get result array, try to remove bot and then add again to the group)

Parse the reponse JSON you can see something like: {"id":-123456789,"title":"tuledev","type":"group","all_members_are_administrators":true}

**-123456789** is the ID of the group.

Then you have to paste bot token and chat id in the config.json file, in the follogin section:

    "telegram": {
        "apiAccessToken": "API_ACCESS_TOKEN",
        "chatId": "CHAT_ID"
    }
