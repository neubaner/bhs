# Teste BHS

Está é uma API desenvolvida em .NET Core que permite de forma simples gerenciar as vendas de veículos de uma concessionária.

## Notas

- O projeto possui uma coleção do Postman, que está localizada na raiz do projeto em um arquivo chamado _BHS.postman_collection.json_.
- Como o controller do veículo e dos vendedores eram simples CRUD, decide utilizar o gerador de controllers do Visual Studio. Já o controller de vendas foi totalmente feito à mão.

## Melhorias

Queria agilizar o processo e entregar o teste o mais cedo possível, então gostaria de menciar uma melhoria que eu faria no projeto.

No momento, todas as regras de negócio referentes à vendas estão no Controller. Por isso eu criaria uma classe de serviço para colocar todas essas regras e limpar o controller da aplicação.

Neste [outro projeto](https://github.com/neubaner/auction-test) eu apliquei essa técnica que mencionei, e também é um projeto mais completo com migrações, autenticação e conexão com banco de dados reais. Se quiserem sintam se livres para explorar ele também.
