CoinT.Net
============

![alt text](main.png "Logo Title Text 1")

### Introduction

CoinT.Net is a basic trading application for bitcoins and other altcoins, which enables users to:
- visualise ticker/candles from exchanges (Bitstamp and BTC-e for now), with basic indicators (MA, EMA)
- send buy/sell orders to these exchanges for multiple currencies
- visualise the order book
- back test trading strategies (EMA crossing and MACD for now)
- Retrieve news items from RSS feeds and Twitter

There is also a feature to check on arbitrage opportunities within BTC-e's multiple currencies (but it's just paper trading for now)

This application was mainly inspired by this project:

[codingdna2/easybot](https://github.com/codingdna2/easybot)


### Source Code

CoinT.Net is a Winforms application, developed in C# and Visual Studio 2013 Express (.Net 4.5). The following external libraries are used

[migrap/BitcoinCharts](https://github.com/migrap/BitcoinCharts)

[DmT021/BtceApi](https://github.com/DmT021/BtceApi)

### Technical notes

It should not be too hard to add more exchanges, then just need to implement the IExchange interface

The currency pairs that can be traded on BTC-e are hard-coded for now, but it's easy to add more


### TODO List

- Add more exchanges and currencies
- Add real time trading trading (it's pretty much finished, but has not been tested)
- Add more indicators
- Add more trading strategies
- Use streaming APIs when available

### Setup

There is a configuration file named CoinTNet.exe.config in the executable folder. With that file, you can configure:
- Bitstamp's API parameters (needed if you want to send orders)
- BTC-e API's parameters (needed if you want to send orders)
- Twitter's parameters (needed if you want to retrieve Tweets)

### Warning

- The keys/secrets in the config are not encrypted, meaning that anyone who has access to your computer could steal these keys.
- The source code is provided as-is. There might be some bugs, so advise you to review the code before using it, especially before making trade orders.



### Donations

If you find this tool useful, you can show you support with a kind donation:

BTC: 1JctmffLPQtcmTSBEDCDquGDeMprfqxX1k