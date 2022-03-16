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
