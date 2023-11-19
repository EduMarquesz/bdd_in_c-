using System;
using Npgsql;

class Cliente
{
    protected NpgsqlConnection con;

    public Cliente(NpgsqlConnection con)
    {
        this.con = con;
    }

    public void Create()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Cliente(id SERIAL PRIMARY KEY, nome VARCHAR(50), cpf VARCHAR(14), endereco VARCHAR(100));";
            cmd.ExecuteNonQuery();
        }
    }

    public void Insert(string nome, string cpf, string endereco)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Cliente(nome, cpf, endereco) VALUES('{nome}', '{cpf}', '{endereco}');";
            cmd.ExecuteNonQuery();
        }
    }

    public void Read()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Cliente;";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32(0)}, Nome: {reader.GetString(1)}, CPF: {reader.GetString(2)}, Endereço: {reader.GetString(3)}");
                }
            }
        }
    }

    public void Update(int id, string nome)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Cliente SET nome = '{nome}' WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM Cliente WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }
}

class Vendedor : Cliente
{
    public Vendedor(NpgsqlConnection con) : base(con)
    {
    }

    public new void Create()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Vendedor(id SERIAL PRIMARY KEY, nome VARCHAR(50), cpf VARCHAR(14), endereco VARCHAR(100));";
            cmd.ExecuteNonQuery();
        }
    }

    public new void Insert(string nome, string cpf, string endereco)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Vendedor(nome, cpf, endereco) VALUES('{nome}', '{cpf}', '{endereco}');";
            cmd.ExecuteNonQuery();
        }
    }

    public new void Read()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Vendedor;";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32(0)}, Nome: {reader.GetString(1)}, CPF: {reader.GetString(2)}, Endereço: {reader.GetString(3)}");
                }
            }
        }
    }

    public new void Update(int id, string nome)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Vendedor SET nome = '{nome}' WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }

    public new void Delete(int id)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM Vendedor WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }
}

class Produto : Cliente
{
    public Produto(NpgsqlConnection con) : base(con)
    {
    }

    public new void Create()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Produto(id SERIAL PRIMARY KEY, nome VARCHAR(50), preco DECIMAL(50,2), categoria VARCHAR(50));";
            cmd.ExecuteNonQuery();
        }
    }

    public void Insert(string nome, string preco, string categoria)
    {
        Console.WriteLine("O valor de preco é: " + preco);
        try
        {
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = $"INSERT INTO Produto(nome, preco, categoria) VALUES('{nome}', {preco}, '{categoria}');";
                cmd.ExecuteNonQuery();
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Erro ao inserir no banco de dados: {ex.Message}");
        }
    }

    public void Read()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Produto;";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32(0)}, Nome: {reader.GetString(1)}, Preço: {reader.GetDecimal(2)}, Categoria: {reader.GetString(3)}");
                }
            }
        }
    }

    public void Update(int id, string nome)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Produto SET nome = '{nome}' WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM Produto WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }
}


class Carrinho : Cliente
{
    public Carrinho(NpgsqlConnection con) : base(con)
    {
    }

    public new void Create()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Carrinho(id SERIAL PRIMARY KEY, clienteId INT REFERENCES Cliente(id), vendedorId INT REFERENCES Vendedor(id), produtoId INT REFERENCES Produto(id));";
            cmd.ExecuteNonQuery();
        }
    }

    public void Insert(int clienteId, int vendedorId, int produtoId)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Carrinho(clienteId, vendedorId, produtoId) VALUES({clienteId}, {vendedorId}, {produtoId});";
            cmd.ExecuteNonQuery();
        }
    }

    public void Read()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Carrinho;";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32(0)}, Cliente ID: {reader.GetInt32(1)}, Vendedor ID: {reader.GetInt32(2)}, Produto ID: {reader.GetInt32(3)}");
                }
            }
        }
    }

    public void Update(int id, int clienteId)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Carrinho SET clienteId = {clienteId} WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM Carrinho WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }
}

class Repositorio : Cliente
{
    public Repositorio(NpgsqlConnection con) : base(con)
    {
    }

    public new void Create()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Repositorio(id SERIAL PRIMARY KEY, carrinhoId INT REFERENCES Carrinho(id), produtoId INT REFERENCES Produto(id));";
            cmd.ExecuteNonQuery();
        }
    }

    public void Insert(int carrinhoId, int produtoId)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Repositorio(carrinhoId, produtoId) VALUES({carrinhoId}, {produtoId});";
            cmd.ExecuteNonQuery();
        }
    }

    public void Read()
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Repositorio;";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32(0)}, Carrinho ID: {reader.GetInt32(1)}, Produto ID: {reader.GetInt32(2)}");
                }
            }
        }
    }

    public void Update(int id, int carrinhoId)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Repositorio SET carrinhoId = {carrinhoId} WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var cmd = new NpgsqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM Repositorio WHERE id = {id};";
            cmd.ExecuteNonQuery();
        }
    }
}


class Program
{
    static void Main()
    {
        string cs = "Host=localhost;Port=5433;Username=postgres;Password=123;Database=marketplace";

        using var con = new NpgsqlConnection(cs);
        con.Open();

        Console.WriteLine("PostgreSQL version: " + con.PostgreSqlVersion.ToString());

        var cliente = new Cliente(con);
        cliente.Create();
        cliente.Insert("Kleber", "638.740.438-00", "Rua jkpop, 5220");
        cliente.Read();
        cliente.Update(1, "José");
        cliente.Delete(1);

        var vendedor = new Vendedor(con);
        vendedor.Create();
        vendedor.Insert("Helio", "528.708.108-14", "Rua manual, 475");
        vendedor.Read();
        vendedor.Update(1, "Ana");
        vendedor.Delete(1);

        var produto = new Produto(con);
        produto.Create();
        produto.Insert("Iphone 15", "5000.00", "Eletronicos");
        produto.Read();
        produto.Update(2, "Iphone 15 Pro Max");
        produto.Delete(1);

        var carrinho = new Carrinho(con);
        carrinho.Create();
        carrinho.Insert(4, 4, 2);
        carrinho.Read();
        carrinho.Update(1, 2);
        carrinho.Delete(1);

        var repositorio = new Repositorio(con);
        repositorio.Create();
        repositorio.Insert(1, 2);
        repositorio.Read();
        repositorio.Update(3, 2);
        repositorio.Delete(1);
    }
}

