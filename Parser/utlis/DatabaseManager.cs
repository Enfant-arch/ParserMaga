using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using Npgsql;
using Parser.Parser.Global;
using Parser.Parser.GoodSorted;

namespace Parser.utlis;
/*
 * Good Id
 * Name
 * Description
 * PRICE
 * BONUS AMOUNT
 * BONUS PERCENT
 * OldPricePercent
 * URL PHOTO
 * URL PRODUCT 
 *
 * CREATE TABLE IF NOT EXISTS products (
    id  SERIAL  PRIMARY KEY,
    goods_id VARCHAR(50) NOT NULL,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    image_url VARCHAR(255),
    product_url VARCHAR(255),
    price DECIMAL(10,2) NOT NULL,
    bonus_amount INT NOT NULL,
    bonus_percent INT NOT NULL,
    old_price_percent INT NOT NULL
);*/


public class DatabaseManager
{
    

    public static async Task InsertProductAsync(Items product)
    {
        Good goods = product.DeserializeGood();
        
        using var connection = new NpgsqlConnection(database.ConnectString);

        var sql = @"INSERT INTO products (goods_id, title, description, image_url, product_url, price, bonus_amount," +
                  " bonus_percent, old_price_percent ) VALUES (@good_id, @title, @description, @image_url, @product_url," +
                  "@price, @bonus_amount, @bonus_percent, @old_price_percent)";

        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@good_id", value:goods.goodsId);
            command.Parameters.AddWithValue("@title", goods.title);
            command.Parameters.AddWithValue("@description", goods.description);
            command.Parameters.AddWithValue("@image_url", goods.titleImage);
            command.Parameters.AddWithValue("@product_url", goods.webUrl);
            command.Parameters.AddWithValue("@price", product.price);
            command.Parameters.AddWithValue("@bonus_amount", product.bonusAmount);
            command.Parameters.AddWithValue("@bonus_percent", product.bonusPercent);
            command.Parameters.AddWithValue("@old_price_percent", product.oldPriceChangePercentage);
            try
            {
                await connection.OpenAsync();
                int affectedRows = command.ExecuteNonQuery();
                await connection.CloseAsync();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }
    }
}