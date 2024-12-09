using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ConvertidorArray2DCadena : JsonConverter<string[,]>
{
    public override string[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jaggedArray = JsonSerializer.Deserialize<string[][]>(ref reader, options);
        return jaggedArray != null ? ConvertirA2D(jaggedArray) : new string[0, 0];
    }

    public override void Write(Utf8JsonWriter writer, string[,] valor, JsonSerializerOptions options)
    {
        var jaggedArray = ConvertToJagged(valor);
        JsonSerializer.Serialize(writer, jaggedArray, options);
    }

    private string[][] ConvertToJagged(string[,] array)
    {
        int filas = array.GetLength(0);
        int columnas = array.GetLength(1);
        var resultado = new string[filas][];

        for (int i = 0; i < filas; i++)
        {
            resultado[i] = new string[columnas];
            for (int j = 0; j < columnas; j++)
            {
                resultado[i][j] = array[i, j];
            }
        }

        return resultado;
    }

    private string[,] ConvertirA2D(string[][] array)
    {
        int filas = array.Length;
        int columnas = array[0].Length;
        var resultado = new string[filas, columnas];

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                resultado[i, j] = array[i][j];
            }
        }

        return resultado;
    }
}