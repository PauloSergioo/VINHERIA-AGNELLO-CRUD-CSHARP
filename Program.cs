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
            Console.Clear();
            ShowHeader("VINHERIA AGNELLO - Sistema Console");
            Console.WriteLine("1 - CRUD Vinhos");
            Console.WriteLine("2 - CRUD Usuários");
            Console.WriteLine("3 - Realizar Compra");
            Console.WriteLine("4 - Listar Compras");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();
            opc = ReadInt("Opção: ");
            switch (opc)
            {
                case 1: MenuVinhos(); break;
                case 2: MenuUsuarios(); break;
                case 3: RealizarCompra(); break;
                case 4: ListarCompras(); break;
                case 0:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nSaindo... Até logo!");
                    Console.ResetColor();
                    break;
                default:
                    ShowError("Opção inválida.");
                    Pause();
                    break;
            }
        } while (opc != 0);
    }

    // ----------------- MENUS -----------------
    static void MenuVinhos()
    {
        int op;
        do
        {
            Console.Clear();
            ShowHeader("MENU DE VINHOS");
            Console.WriteLine("1 - Cadastrar Vinho");
            Console.WriteLine("2 - Listar Vinhos");
            Console.WriteLine("3 - Atualizar Vinho");
            Console.WriteLine("4 - Deletar Vinho");
            Console.WriteLine("0 - Voltar\n");
            op = ReadInt("Opção: ");
            switch (op)
            {
                case 1: CadastrarVinho(); break;
                case 2: ListarVinhos(); break;
                case 3: AtualizarVinho(); break;
                case 4: DeletarVinho(); break;
                case 0: break;
                default:
                    ShowError("Opção inválida.");
                    Pause();
                    break;
            }
        } while (op != 0);
    }

    static void MenuUsuarios()
    {
        int op;
        do
        {
            Console.Clear();
            ShowHeader("MENU DE USUÁRIOS");
            Console.WriteLine("1 - Cadastrar Usuário");
            Console.WriteLine("2 - Listar Usuários");
            Console.WriteLine("3 - Atualizar Usuário");
            Console.WriteLine("4 - Deletar Usuário");
            Console.WriteLine("0 - Voltar\n");
            op = ReadInt("Opção: ");
            switch (op)
            {
                case 1: CadastrarUsuario(); break;
                case 2: ListarUsuarios(); break;
                case 3: AtualizarUsuario(); break;
                case 4: DeletarUsuario(); break;
                case 0: break;
                default:
                    ShowError("Opção inválida.");
                    Pause();
                    break;
            }
        } while (op != 0);
    }

    // ---------------- VINHOS ----------------
    static void CadastrarVinho()
    {
        Console.Clear();
        ShowHeader("CADASTRAR VINHO");

        var v = new Vinho
        {
            Nome = ReadString("Nome: "),
            Marca = ReadString("Marca: "),
            PaisOrigem = ReadString("País origem: "),
            TipoUva = ReadString("Tipo uva: "),
            TempoEnvelhecimento = ReadString("Tempo envelhecimento: "),
            Tipo = ReadString("Tipo: "),
            Descricao = ReadString("Descrição: "),
            PerfilSabor = ReadString("Perfil sabor: "),
            Ocasiao = ReadString("Ocasião: "),
            Harmonizacao = ReadString("Harmonização: "),
            Preco = ReadDouble("Preço: "),
            UrlImagem = ReadString("URL imagem: ")
        };

        try
        {
            vinhoDAO.Inserir(v);
            ShowSuccess("Vinho cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao inserir vinho: " + ex.Message);
        }

        Pause();
    }

    static void ListarVinhos()
    {
        Console.Clear();
        ShowHeader("LISTA DE VINHOS");

        try
        {
            var list = vinhoDAO.Listar();
            Console.WriteLine($"{"ID",-5} {"Nome",-30} {"Marca",-20} {"Preço",-10}");
            Console.WriteLine(new string('-', 75));
            foreach (var v in list)
                Console.WriteLine($"{v.IdVinho,-5} {Truncate(v.Nome,30),-30} {Truncate(v.Marca,20),-20} R${v.Preco:0.00}");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao listar vinhos: " + ex.Message);
        }

        Pause();
    }

    static void AtualizarVinho()
    {
        Console.Clear();
        ShowHeader("ATUALIZAR VINHO");

        int id = ReadInt("ID do vinho: ");
        try
        {
            var v = vinhoDAO.GetById(id);
            if (v == null) { ShowError("Vinho não encontrado."); Pause(); return; }

            Console.WriteLine("\n(Deixe em branco para manter o valor atual)");
            string s = ReadOptionalString($"Nome ({v.Nome}): "); if (!string.IsNullOrEmpty(s)) v.Nome = s;
            s = ReadOptionalString($"Marca ({v.Marca}): "); if (!string.IsNullOrEmpty(s)) v.Marca = s;
            s = ReadOptionalString($"País origem ({v.PaisOrigem}): "); if (!string.IsNullOrEmpty(s)) v.PaisOrigem = s;
            s = ReadOptionalString($"Tipo uva ({v.TipoUva}): "); if (!string.IsNullOrEmpty(s)) v.TipoUva = s;
            s = ReadOptionalString($"Tempo envelhecimento ({v.TempoEnvelhecimento}): "); if (!string.IsNullOrEmpty(s)) v.TempoEnvelhecimento = s;
            s = ReadOptionalString($"Tipo ({v.Tipo}): "); if (!string.IsNullOrEmpty(s)) v.Tipo = s;
            s = ReadOptionalString($"Descrição ({Truncate(v.Descricao,40)}): "); if (!string.IsNullOrEmpty(s)) v.Descricao = s;
            s = ReadOptionalString($"Perfil sabor ({v.PerfilSabor}): "); if (!string.IsNullOrEmpty(s)) v.PerfilSabor = s;
            s = ReadOptionalString($"Ocasião ({v.Ocasiao}): "); if (!string.IsNullOrEmpty(s)) v.Ocasiao = s;
            s = ReadOptionalString($"Harmonização ({v.Harmonizacao}): "); if (!string.IsNullOrEmpty(s)) v.Harmonizacao = s;
            string precoStr = ReadOptionalString($"Preço ({v.Preco}): "); if (!string.IsNullOrEmpty(precoStr) && double.TryParse(precoStr, out var p)) v.Preco = p;
            s = ReadOptionalString($"URL imagem ({v.UrlImagem}): "); if (!string.IsNullOrEmpty(s)) v.UrlImagem = s;

            vinhoDAO.Atualizar(v);
            ShowSuccess("Vinho atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao atualizar vinho: " + ex.Message);
        }

        Pause();
    }

    static void DeletarVinho()
    {
        Console.Clear();
        ShowHeader("DELETAR VINHO");

        int id = ReadInt("ID do vinho: ");
        Console.Write("Tem certeza que deseja deletar? (S/N): ");
        if (!Confirm()) { ShowError("Operação cancelada."); Pause(); return; }

        try
        {
            vinhoDAO.Deletar(id);
            ShowSuccess("Vinho removido com sucesso!");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao deletar vinho: " + ex.Message);
        }

        Pause();
    }

    // ---------------- USUÁRIOS ----------------
    static void CadastrarUsuario()
    {
        Console.Clear();
        ShowHeader("CADASTRAR USUÁRIO");

        var u = new Usuario
        {
            Nome = ReadString("Nome: "),
            Email = ReadString("Email: "),
            Senha = ReadString("Senha: ")
        };

        try
        {
            usuarioDAO.Inserir(u);
            ShowSuccess("Usuário cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao cadastrar usuário: " + ex.Message);
        }

        Pause();
    }

    static void ListarUsuarios()
    {
        Console.Clear();
        ShowHeader("LISTA DE USUÁRIOS");

        try
        {
            var list = usuarioDAO.Listar();
            Console.WriteLine($"{"ID",-5} {"Nome",-30} {"Email",-30}");
            Console.WriteLine(new string('-', 70));
            foreach (var u in list)
                Console.WriteLine($"{u.IdUsuario,-5} {Truncate(u.Nome,30),-30} {Truncate(u.Email,30),-30}");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao listar usuários: " + ex.Message);
        }

        Pause();
    }

    static void AtualizarUsuario()
    {
        Console.Clear();
        ShowHeader("ATUALIZAR USUÁRIO");

        int id = ReadInt("ID do usuário: ");
        try
        {
            var u = usuarioDAO.GetById(id);
            if (u == null) { ShowError("Usuário não encontrado."); Pause(); return; }

            string s = ReadOptionalString($"Nome ({u.Nome}): "); if (!string.IsNullOrEmpty(s)) u.Nome = s;
            s = ReadOptionalString($"Email ({u.Email}): "); if (!string.IsNullOrEmpty(s)) u.Email = s;
            s = ReadOptionalString($"Senha (******): "); if (!string.IsNullOrEmpty(s)) u.Senha = s;

            usuarioDAO.Atualizar(u);
            ShowSuccess("Usuário atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao atualizar usuário: " + ex.Message);
        }

        Pause();
    }

    static void DeletarUsuario()
    {
        Console.Clear();
        ShowHeader("DELETAR USUÁRIO");

        int id = ReadInt("ID do usuário a deletar: ");
        Console.Write("Tem certeza que deseja deletar? (S/N): ");
        if (!Confirm()) { ShowError("Operação cancelada."); Pause(); return; }

        try
        {
            usuarioDAO.Deletar(id);
            ShowSuccess("Usuário removido com sucesso!");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao deletar usuário: " + ex.Message);
        }

        Pause();
    }

    // ---------------- COMPRAS ----------------
    static void RealizarCompra()
    {
        Console.Clear();
        ShowHeader("REALIZAR COMPRA");

        // Selecionar usuário (com listagem)
        try
        {
            var usuarios = usuarioDAO.Listar();
            Console.WriteLine("=== USUÁRIOS ===");
            foreach (var u in usuarios)
                Console.WriteLine($"{u.IdUsuario} - {Truncate(u.Nome,25)} ({Truncate(u.Email,25)})");
            Console.WriteLine();

            int uid = ReadInt("Escolha ID do usuário comprador: ");
            var user = usuarioDAO.GetById(uid);
            if (user == null) { ShowError("Usuário inválido."); Pause(); return; }

            // Selecionar vinho
            var vinhos = vinhoDAO.Listar();
            Console.WriteLine("\n=== VINHOS ===");
            foreach (var v in vinhos)
                Console.WriteLine($"{v.IdVinho} - {Truncate(v.Nome,30)} ({Truncate(v.Marca,20)}) - R${v.Preco:0.00}");
            Console.WriteLine();

            int vid = ReadInt("Escolha ID do vinho: ");
            var vinho = vinhoDAO.GetById(vid);
            if (vinho == null) { ShowError("Vinho inválido."); Pause(); return; }

            int qtd = ReadInt("Quantidade: ");
            if (qtd <= 0) { ShowError("Quantidade inválida."); Pause(); return; }

            // Tenta inserir compra com opção de tentar novamente se erro de BD
            bool sucesso = false;
            while (!sucesso)
            {
                try
                {
                    compraDAO.InserirCompra(uid, vid, qtd);
                    ShowSuccess($"Compra registrada! Total: R${vinho.Preco * qtd:0.00}");
                    sucesso = true;
                }
                catch (Exception ex)
                {
                    ShowError("Erro ao registrar compra: " + ex.Message);
                    Console.Write("Deseja tentar novamente? (S/N): ");
                    if (!Confirm()) break;
                }
            }
        }
        catch (Exception ex)
        {
            ShowError("Erro no fluxo de compra: " + ex.Message);
        }

        Pause();
    }

    static void ListarCompras()
    {
        Console.Clear();
        ShowHeader("HISTÓRICO DE COMPRAS");

        try
        {
            var list = compraDAO.Listar();
            Console.WriteLine($"{"ID",-4} {"Data",-20} {"Usuário",-10} {"Vinho",-10} {"Qtd",-4} {"Total",-10}");
            Console.WriteLine(new string('-', 70));

            foreach (var c in list)
                Console.WriteLine($"{c.IdCompra,-4} {c.Data,-20} {c.UsuarioId,-10} {c.VinhoId,-10} {c.Quantidade,-4} R${c.Total:0.00}");
        }
        catch (Exception ex)
        {
            ShowError("Erro ao listar compras: " + ex.Message);
        }

        Pause();
    }

    // ---------------- AUXILIARES ----------------
    static void ShowHeader(string title)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('=', 50));
        Console.WriteLine($"   {title}");
        Console.WriteLine(new string('=', 50));
        Console.ResetColor();
        Console.WriteLine();
    }

    static void ShowSuccess(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n {msg}");
        Console.ResetColor();
    }

    static void ShowError(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n {msg}");
        Console.ResetColor();
    }

    static void Pause()
    {
        Console.WriteLine("\nPressione Enter para continuar...");
        Console.ReadLine();
    }

    static bool Confirm()
    {
        var resp = Console.ReadLine() ?? "";
        return resp.Trim().ToUpper() == "S";
    }

    static int ReadInt(string prompt)
    {
        int valor;
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out valor)) return valor;
            ShowError("Valor inválido. Digite um número inteiro.");
        }
    }

    static double ReadDouble(string prompt)
    {
        double valor;
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (double.TryParse(input, out valor)) return valor;
            ShowError("Valor inválido. Digite um número (ex: 89.90).");
        }
    }

    static string ReadString(string prompt)
    {
        string? valor;
        do
        {
            Console.Write(prompt);
            valor = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(valor)) ShowError("Campo obrigatório. Digite um valor.");
        } while (string.IsNullOrWhiteSpace(valor));
        return valor!.Trim();
    }

    static string ReadOptionalString(string prompt)
    {
        Console.Write(prompt);
        return (Console.ReadLine() ?? "").Trim();
    }

    static string Truncate(string? s, int max)
    {
        if (string.IsNullOrEmpty(s)) return "";
        return s.Length <= max ? s : s.Substring(0, max - 3) + "...";
    }
}