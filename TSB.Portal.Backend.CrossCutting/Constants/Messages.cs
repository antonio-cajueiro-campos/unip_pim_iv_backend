
namespace TSB.Portal.Backend.CrossCutting.Constants
{
    public static class Messages
    {
		// Requests messages
		public const string Created = "Criado com sucesso!";
		public const string Deleted = "Apagado com sucesso!";
		public const string Updated = "Atualizado com sucesso!";
		public const string Executed = "Executado com sucesso!";
		public const string NotExecuted = "Falha na execução.";
		public const string Success = "Operação realizada com sucesso!";
		public const string BadRequest = "Parâmetros incorretos.";
		public const string Authenticated = "Autorizado com sucesso!";
		public const string InvalidOperation = "Operação inválida";
		public const string Error = "Oops! Algo deu errado e foi gerado o seguinte erro no servidor: ";
		public const string Unauthorized = "Não autorizado.";
		public const string UserNotFound = "Usuário não encontrado.";
		public static string UsernameAlreadyTaken(string username) => $"Usuário {username} já está em uso.";
		public static string DocumentAlreadyTaken(string document) => $"Documento {document} já está em uso.";

		// Fields messages
		public const string RequiredUsername = "Preencha o campo Usuário.";
		public const string RequiredPassword = "Preencha o campo Senha.";
		public const string RequiredRepassword = "Preencha o campo Repetir Senha.";
		public const string PasswordsNotMatch = "As senhas não coincidem.";
		public const string RequiredName = "Preencha o campo Nome.";
		public const string RequiredDocument = "Preencha o campo Documento.";
    }
}
