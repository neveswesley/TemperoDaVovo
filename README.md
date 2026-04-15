# NvsFood | Tempero da Vovó

Sistema full stack para restaurantes com delivery que automatiza completamente o processo de pedidos, eliminando a necessidade de atendimento manual via WhatsApp.

## Visão Geral

O NvsFood foi desenvolvido para resolver um problema comum em pequenos e médios restaurantes: a dependência de atendimento manual para receber pedidos.
Com a plataforma, o próprio cliente monta seu pedido de forma autônoma, enquanto o restaurante recebe tudo estruturado, pronto para produção e entrega.

O resultado é mais agilidade, menos erros e um fluxo de pedidos muito mais profissional.

## Acesso ao Sistema

* Repositório: https://github.com/neveswesley/TemperoDaVovo
* Painel do restaurante: https://nvsfood.vercel.app/login
* Área do cliente: https://nvsfood.vercel.app/delivery-home/089364D2-0D9F-48E9-9535-F31CF78A3D5F

## Tecnologias Utilizadas

* Back-end: C# com ASP.NET
* Front-end: Angular
* Banco de dados: PostgreSQL
* Autenticação: JWT
* Arquitetura: API REST + aplicação SPA

## Principais Funcionalidades

### Cliente

* Visualização de produtos com:

  * Nome, descrição, preço e imagem
* Seleção de complementos:

  * Obrigatórios ou opcionais, definidos pelo restaurante
* Escolha de endereço com base em bairros cadastrados
* Cálculo de taxa de entrega automática
* Escolha da forma de pagamento
* Finalização de pedido sem interação com atendente
* Acompanhamento do status do pedido em tempo real

### Restaurante

* Cadastro e autenticação de restaurante (JWT)
* Atualização de dados do restaurante:

  * Nome (restrição de alteração a cada 14 dias)
* Cadastro de bairros atendidos
* Definição de taxa de entrega por região
* Configuração de retirada no local (opcional)
* Gestão de produtos:

  * Nome, descrição, preço, etc.
* Gestão de pedidos:

  * Aceitar ou recusar pedidos
  * Atualizar status (em preparo, despachado, etc.)

## Diferencial do Projeto

O sistema substitui comeepletamente o fluxo informal de pedidos via WhatsApp por uma experiência estruturada e escalável, mantendo simplicidade para o cliente final e controle total para o restaurante.

Esse tipo de solução é diretamente aplicável a negócios reais, especialmente pequenos restaurantes que precisam profissionalizar o atendimento sem aumentar custos operacionais.

## Como Executar o Projeto

### Back-end (ASP.NET)

```bash
dotnet run
```

### Front-end (Angular)

```bash
ng serve
```

Certifique-se de que o banco PostgreSQL esteja configurado corretamente e que as variáveis de ambiente estejam ajustadas conforme o projeto.

## Autenticação

* Sistema baseado em JWT
* Endpoints protegidos para operações sensíveis
* Controle de acesso para ações do restaurante

## Estrutura do Sistema

* API responsável por regras de negócio, autenticação e persistência
* Front-end Angular consumindo a API
* Separação clara entre fluxo do cliente e fluxo do restaurante

## Possíveis Evoluções

* Integração com meios de pagamento online
* Notificações em tempo real (WebSocket / SignalR)
* Dashboard com métricas de vendas
* Sistema de cupons e promoções
* Multi-restaurantes (modelo SaaS completo)

## Autor

Wesley Luan das Neves Miranda

Projeto desenvolvido com foco em prática real de desenvolvimento full stack, arquitetura de APIs e construção de sistemas aplicáveis ao mercado.
