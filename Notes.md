# Anotações

## Segurança de APIs

### Configurando IdentityServer4

Criando o projeto:

```bash
# Criando o projeto base
mkdir CursoIS4
cd CursoIS4
dotnet new mvc

# instalando os templates is4
dotnet new -i identityserver4.templates

# Utilizando o template "IdentityServer4 with AdminUI"
# Obs.: "--force" é utilizado porque o template vai modificar alguns arquivos do projeto
dotnet new is4admin --force
```

Os templates estão bem desatualizados, aparentemente o projeto [IdentityServer4.Templates](https://github.com/IdentityServer/IdentityServer4.Templates) está utilizando a versão 3.1 do dotnet e vai ser descontinuado em Dezembro de 2022. Portanto para prosseguir com o curso eu criei um novo "global.json" para o projeto utilizando o comando  `dotnet new globaljson` e configurei para utilizar a versão 3.1.1XX do dotnet.

No linux eu tive que instalar o sdk 3.1 com o script da microsoft (arch btw):

```bash
# Baixando o script de instalação da microsoft
wget https://dot.net/v1/dotnet-install.sh

# Tornando o script executável
chmod +x ./dotnet-install.sh

# Instalando o sdk 3.1 com o script
# Obs.: utilizando os sdks do AUR arch linux
sudo ./dotnet-install.sh -c 3.1 --install-dir /usr/share/dotnets
```

## OpenId Connect com IdentityServer4

### Implementando Authorization Code

Estão ocorrendo problemas com ssl, eu permiti certificados localhost no chrome e não funcionou...

Coloquei também a opção `options.RequireHttpsMetadata = false;` no startup do projeto "Site" e troquei para utilizar http e agora entra na página de login, porém após digitar a senha correta (User: bob, Password: Bob123!!) não consegui logar, aparentemente não dá erro, mas não redireciona. `:(`

Reativando o ssl e rodando no windows funcionou perfeitamente, o problema provavelmente é relacionado ao certificado auto assinado no linux, devo ter cometido algum erro instalando. TODO: Revisar instalação do certificado autoassinado dotnetcore no archlinux.

Para executar o projeto no vscode, selecione "Módulo 4 All" nas opções do debugger do vscode.

#### Update

Corrigido o problema no linux! O problema realmente era certificado autoassinado, agora para rodar esse projeto no linux, é necessário criar um certificado em `~/.aspnet/https/localhost.pfx` sem senha (ou alterar a senha no launcher.json) e fazer o linux aceitar esse certificado.

Para facilitar eu usei o script [dev-cert.sh](https://github.com/amadoa/dotnet-devcert-linux/blob/master/dev-cert.sh) do repositório github [amadoa/dotnet-devcert-linux](https://github.com/amadoa/dotnet-devcert-linux).

E depois para fazer o linux "acreditar"/"trust" o certificado, eu utilizei o comando trust do pacote [p11-kit](https://archlinux.org/packages/core/x86_64/p11-kit/) do archlinux.

Exemplo:

```bash
# Baixando o script para gerar o certificado
wget https://raw.githubusercontent.com/amadoa/dotnet-devcert-linux/master/dev-cert.sh

# Criando o diretório de certificados manualmente porque o script não cria
mkdir -p ~/.aspnet/https

# Tornando o script executável
chmod +x dev-cert.sh

# Executando o script para gerar o certificado
./dev-cert.sh

# Removendo o script
rm dev-cert.sh

# Fazendo o linux "acreditar" o certificado
sudo trust anchor $HOME/.aspnet/https/ca.crt
```

Obs.: pode ser necessário reiniciar o chrome ou mesmo o computador para que o certificado seja reconhecido.

## Protegendo APIs com IdentityServer4

### Protegendo a API

Até o momento tudo certo, as configurações foram feitas igual no módulo 4, porém o usuário bob agora está com os dados padrões dele (igual ao curso), nem sei qual é a senha dele, mas como já aparece "auto completada" pela própria aplicação como exemplo, é só clicar em logar direto que vai logar com o usuário bob.

Obs.: Para executar o projeto no vscode, selecione "Módulo 5 All" nas opções do debugger do vscode. E se estiver no linux siga as instruções do módulo 4 sobre o certificado auto assinado ssl.
