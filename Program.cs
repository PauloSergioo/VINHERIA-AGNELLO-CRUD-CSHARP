using System;
using VinheriaAgnelloCRUD.DAO;
using VinheriaAgnelloCRUD.Models;

class Program
{
    static VinhoDAO vinhoDAO = new VinhoDAO();
    static UsuarioDAO usuarioDAO = new UsuarioDAO();
    static CompraDAO compraDAO = new CompraDAO();

    static void Main()
    {
        int opc;
        do
        {
            ShowMainMenu();
            opc = ReadInt("Opção: ");
            switch (opc)
            {
                case 1: MenuVinhos(); break;
                case 2: MenuUsuarios(); break;
                case 3: RealizarCompra(); break;
                case 4: ListarCompras(); break;
                case 0: Console.WriteLine("Saindo..."); break;
                default: Console.WriteLine("Opção inválida"); break;
            }
        } while (opc != 0);
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("\n=== VINHERIA AGNELLO ===");
        Console.WriteLine("1 - CRUD Vinhos");
        Console.WriteLine("2 - CRUD Usuários");
        Console.WriteLine("3 - Realizar Compra");
        Console.WriteLine("4 - Listar Compras");
        Console.WriteLine("0 - Sair");
    }

    // ---------------- Vinhos ----------------
    static void MenuVinhos()
    {
        int op;
        do
        {
            Console.WriteLine("\n-- VINHOS --");
            Console.WriteLine("1 - Cadastrar Vinho");
            Console.WriteLine("2 - Listar Vinhos");
            Console.WriteLine("3 - Atualizar Vinho");
            Console.WriteLine("4 - Deletar Vinho");
            Console.WriteLine("0 - Voltar");
            op = ReadInt("Opção: ");
            switch (op)
            {
                case 1: CadastrarVinho(); break;
                case 2: ListarVinhos(); break;
                case 3: AtualizarVinho(); break;
                case 4: DeletarVinho(); break;
            }
        } while (op != 0);
    }

    static void CadastrarVinho()
    {
        var v = new Vinho();
        v.Nome = ReadString("Nome: ");
        v.Marca = ReadString("Marca: ");
        v.PaisOrigem = ReadString("País origem: ");
        v.TipoUva = ReadString("Tipo uva: ");
        v.TempoEnvelhecimento = ReadString("Tempo envelhecimento: ");
        v.Tipo = ReadString("Tipo: ");
        v.Descricao = ReadString("Descrição: ");
        v.PerfilSabor = ReadString("Perfil sabor: ");
        v.Ocasiao = ReadString("Ocasião: ");
        v.Harmonizacao = ReadString("Harmonização: ");
        v.Preco = ReadDouble("Preço: ");
        v.UrlImagem = ReadString("URL imagem: ");
        vinhoDAO.Inserir(v);
    }

    static void ListarVinhos()
    {
        var list = vinhoDAO.Listar();
        Console.WriteLine("\n--- VINHOS ---");
        foreach (var v in list)
            Console.WriteLine($"{v.IdVinho} - {v.Nome} ({v.Marca}) - R${v.Preco:0.00}");
    }

    static void AtualizarVinho()
    {
        int id = ReadInt("ID do vinho a atualizar: ");
        var v = vinhoDAO.GetById(id);
        if (v == null) { Console.WriteLine("Vinho não encontrado."); return; }
        Console.WriteLine("Deixe em branco para manter o valor atual.");
        string s = ReadString($"Nome ({v.Nome}): "); if (!string.IsNullOrEmpty(s)) v.Nome = s;
        s = ReadString($"Marca ({v.Marca}): "); if (!string.IsNullOrEmpty(s)) v.Marca = s;
        s = ReadString($"País origem ({v.PaisOrigem}): "); if (!string.IsNullOrEmpty(s)) v.PaisOrigem = s;
        s = ReadString($"Tipo uva ({v.TipoUva}): "); if (!string.IsNullOrEmpty(s)) v.TipoUva = s;
        s = ReadString($"Tempo envelhecimento ({v.TempoEnvelhecimento}): "); if (!string.IsNullOrEmpty(s)) v.TempoEnvelhecimento = s;
        s = ReadString($"Tipo ({v.Tipo}): "); if (!string.IsNullOrEmpty(s)) v.Tipo = s;
        s = ReadString($"Descrição ({v.Descricao}): "); if (!string.IsNullOrEmpty(s)) v.Descricao = s;
        s = ReadString($"Perfil sabor ({v.PerfilSabor}): "); if (!string.IsNullOrEmpty(s)) v.PerfilSabor = s;
        s = ReadString($"Ocasião ({v.Ocasiao}): "); if (!string.IsNullOrEmpty(s)) v.Ocasiao = s;
        s = ReadString($"Harmonização ({v.Harmonizacao}): "); if (!string.IsNullOrEmpty(s)) v.Harmonizacao = s;
        string precoStr = ReadString($"Preço ({v.Preco}): "); if (!string.IsNullOrEmpty(precoStr) && double.TryParse(precoStr, out var p)) v.Preco = p;
        s = ReadString($"URL imagem ({v.UrlImagem}): "); if (!string.IsNullOrEmpty(s)) v.UrlImagem = s;
        vinhoDAO.Atualizar(v);
    }

    static void DeletarVinho()
    {
        int id = ReadInt("ID do vinho a deletar: ");
        vinhoDAO.Deletar(id);
    }

    // ---------------- Usuários ----------------
    static void MenuUsuarios()
    {
        int op;
        do
        {
            Console.WriteLine("\n-- USUÁRIOS --");
            Console.WriteLine("1 - Cadastrar Usuário");
            Console.WriteLine("2 - Listar Usuários");
            Console.WriteLine("3 - Atualizar Usuário");
            Console.WriteLine("4 - Deletar Usuário");
            Console.WriteLine("0 - Voltar");
            op = ReadInt("Opção: ");
            switch (op)
            {
                case 1: CadastrarUsuario(); break;
                case 2: ListarUsuarios(); break;
                case 3: AtualizarUsuario(); break;
                case 4: DeletarUsuario(); break;
            }
        } while (op != 0);
    }

    static void CadastrarUsuario()
    {
        var u = new Usuario();
        u.Nome = ReadString("Nome: ");
        u.Email = ReadString("Email: ");
        u.Senha = ReadString("Senha: ");
        usuarioDAO.Inserir(u);
    }

    static void ListarUsuarios()
    {
        var list = usuarioDAO.Listar();
        Console.WriteLine("\n--- USUÁRIOS ---");
        foreach (var u in list)
            Console.WriteLine($"{u.IdUsuario} - {u.Nome} ({u.Email})");
    }

    static void AtualizarUsuario()
    {
        int id = ReadInt("ID do usuário: ");
        var u = usuarioDAO.GetById(id);
        if (u == null) { Console.WriteLine("Usuário não encontrado."); return; }
        string s = ReadString($"Nome ({u.Nome}): "); if (!string.IsNullOrEmpty(s)) u.Nome = s;
        s = ReadString($"Email ({u.Email}): "); if (!string.IsNullOrEmpty(s)) u.Email = s;
        s = ReadString($"Senha (*******): "); if (!string.IsNullOrEmpty(s)) u.Senha = s;
        usuarioDAO.Atualizar(u);
    }

    static void DeletarUsuario()
    {
        int id = ReadInt("ID do usuário a deletar: ");
        usuarioDAO.Deletar(id);
    }

    // ---------------- Compras ----------------
    static void RealizarCompra()
    {
        Console.WriteLine("\n-- REALIZAR COMPRA --");
        ListarUsuarios();
        int uid = ReadInt("Escolha ID do usuário comprador: ");
        var user = usuarioDAO.GetById(uid);
        if (user == null) { Console.WriteLine("Usuário inválido."); return; }

        ListarVinhos();
        int vid = ReadInt("Escolha ID do vinho: ");
        var vinho = vinhoDAO.GetById(vid);
        if (vinho == null) { Console.WriteLine("Vinho inválido."); return; }

        int qtd = ReadInt("Quantidade: ");
        if (qtd <= 0) { Console.WriteLine("Quantidade inválida."); return; }

        try
        {
            compraDAO.InserirCompra(uid, vid, qtd);
            Console.WriteLine("Compra finalizada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro na compra: " + ex.Message);
        }
    }

    static void ListarCompras()
    {
        var list = compraDAO.Listar();
        Console.WriteLine("\n--- COMPRAS ---");
        foreach (var c in list)
            Console.WriteLine($"{c.IdCompra} - Data: {c.Data} - Usuário: {c.UsuarioId} - Vinho: {c.VinhoId} - Qtd: {c.Quantidade} - Total: R${c.Total:0.00}");
    }

    // ---------------- Helpers ----------------
    static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int v)) return v;
        return 0;
    }

    static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        if (double.TryParse(Console.ReadLine(), out double v)) return v;
        return 0.0;
    }

    static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }
}
