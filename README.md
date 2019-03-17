# CS481 Midterm Project

Using Xamarin ListView, MicroCharts, & JSON API

## Midterm Instructions

For the CS481 Midterm, you will create a stock market app. You will utilize the Alpha Vantage API. (https://www.alphavantage.co/) With this API you will create a tabbed page app to show historic high and low prices of stocks. Documentation for the API can be found here https://www.alphavantage.co/documentation/  

Use the TIME_SERIES_DAILY API endpoint to get the historical data on stocks.  https://www.alphavantage.co/documentation/#daily  

On one tab you will use a ListView to show at a minimum the historical high and low prices of a given stock. Users should be allowed to enter the given stock symbol that they would like to they would like to get data on. So the page should be able to work with any stock. If a user enters a stock symbol that does not exist the app should handle this and display an alert to the user notifying them of the invalid symbol. This page should also show the highest price and lowest price that is seen in the data for a stock.  

On the second tab, the Microcharts package should be used to display the high price in graph format for at least the past 30 days or more. Only one chart is required but more is always allowed.

Requirements:

- Use the Alpha Vantage API
- Use the TIME_SERIES_DAILY endpoint to gather stock data
- Create a Model to store the JSON as a C# object
- Show at least 30 of the past days high and low prices for stock
- Show the highest and lowest prices for a given stock from the data received
- Allow the user to enter a stock symbol and get data for that stock
- Handle user entering an invalid symbol. Should display an alert notifying the user of invalid stock symbol
- Use Microcharts to display the high price of a stock over at least the past 30 days.
- The app should look clean and organized, put some effort into making it a good looking UI
