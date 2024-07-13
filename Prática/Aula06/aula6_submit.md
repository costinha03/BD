# BD: Guião 6

## Problema 6.1

### *a)* Todos os tuplos da tabela autores (authors);

```
select * from authors;	
```

### *b)* O primeiro nome, o último nome e o telefone dos autores;

```
select au_lname, au_fname, phone from authors;
```

### *c)* Consulta definida em b) mas ordenada pelo primeiro nome (ascendente) e depois o último nome (ascendente); 

```
select au_fname,au_lname, phone from authors order by au_fname, au_lname;

```

### *d)* Consulta definida em c) mas renomeando os atributos para (first_name, last_name, telephone); 

```
select au_fname as first_name, au_lname as last_name, phone as telephone from authors order by au_fname, au_lname; 
```

### *e)* Consulta definida em d) mas só os autores da Califórnia (CA) cujo último nome é diferente de ‘Ringer’; 

```
select au_fname as first_name, au_lname as last_name, phone as telephone from authors
where state='CA' and au_lname <> 'Ringer'
order by au_fname, au_lname; 
```

### *f)* Todas as editoras (publishers) que tenham ‘Bo’ em qualquer parte do nome; 

```
select * from publishers
where pub_name like '%Bo%'; 
```

### *g)* Nome das editoras que têm pelo menos uma publicação do tipo ‘Business’; 

```
select pub_name from publishers, titles
where titles.pub_id = publishers.pub_id and	type = 'Business'
```

### *h)* Número total de vendas de cada editora; 

```
select pub_name, sum(qty) as total_sales from	 ((sales join titles on sales.title_id = titles.title_id join publishers on titles.pub_id = publishers.pub_id)) group by pub_name

```

### *i)* Número total de vendas de cada editora agrupado por título; 

```
select pub_name, sum(qty), title as total_sales from publishers, sales, titles
where publishers.pub_id = titles.pub_id AND
      sales.title_id = titles.title_id
group by pub_name, title	
```

### *j)* Nome dos títulos vendidos pela loja ‘Bookbeat’; 

```
select title
from ((titles join sales on titles.title_id = sales.title_id) join stores on sales.stor_id = stores.stor_id)
where stores.stor_name = 'Bookbeat'
```

### *k)* Nome de autores que tenham publicações de tipos diferentes; 

```
select au_fname as first_name, au_lname as last_name
from titles, authors, titleauthor
where titles.title_id = titleauthor.title_id and authors.au_id = titleauthor.au_id
group by au_fname, au_lname
having count( distinct type) > 1
```

### *l)* Para os títulos, obter o preço médio e o número total de vendas agrupado por tipo (type) e editora (pub_id);

```
select titles.type, avg(titles.price) as avg_price, sum(sales.qty) as total_sales
from (
    titles join sales on titles.title_id = sales.title_id
    )
group by titles.type,titles.pub_id;
```

### *m)* Obter o(s) tipo(s) de título(s) para o(s) qual(is) o máximo de dinheiro “à cabeça” (advance) é uma vez e meia superior à média do grupo (tipo);

```
select type, max(advance) as max_advance, avg(advance) as avg_advance
from titles
group by type
having max(advance)	 > 1.5 * avg(advance) 
```

### *n)* Obter, para cada título, nome dos autores e valor arrecadado por estes com a sua venda;

```
select distinct authors.au_id as 'Author ID', authors.au_fname as 'First Name',authors.au_lname as 'Last Name', titles.title as 'Title', sum(royalty) as 'Total Sales'
from (
    ((titles join sales on titles.title_id = sales.title_id) join titleauthor on titles.title_id = titleauthor.title_id) join authors on titleauthor.au_id = authors.au_id
    )
group by authors.au_id, authors.au_fname,authors.au_lname,titles.title;
```

### *o)* Obter uma lista que incluía o número de vendas de um título (ytd_sales), o seu nome, a faturação total, o valor da faturação relativa aos autores e o valor da faturação relativa à editora;

```
select title, ytd_sales,price*ytd_sales as facturacao, price*ytd_sales*royalty/100 as auths_revenue, price*ytd_sales-price*ytd_sales*royalty/100 as publisher_revenue
from titles;
```

### *p)* Obter uma lista que incluía o número de vendas de um título (ytd_sales), o seu nome, o nome de cada autor, o valor da faturação de cada autor e o valor da faturação relativa à editora;

```
select title, CONCAT(authors.au_fname, ' ', authors.au_lname) as author, price*ytd_sales*royalty/100 as auth_revenue, price*ytd_sales-price*ytd_sales*royalty/100 as publisher_revenue
from (
    (titles join titleauthor on titles.title_id=titleauthor.title_id) join authors on titleauthor.au_id=authors.au_id
    )
```

### *q)* Lista de lojas que venderam pelo menos um exemplar de todos os livros;

```

SELECT stor_name FROM stores INNER JOIN sales ON stores.stor_id=sales.stor_id INNER JOIN titles ON sales.title_id=titles.title_id
GROUP BY stores.stor_name
HAVING COUNT(title)=(SELECT COUNT(title_id) FROM titles);
```

### *r)* Lista de lojas que venderam mais livros do que a média de todas as lojas;

```
SELECT stor_name FROM stores INNER JOIN sales ON stores.stor_id=sales.stor_id INNER JOIN titles ON sales.title_id=titles.title_id
GROUP BY stores.stor_name
HAVING SUM(sales.qty)>(SELECT SUM(sales.qty)/COUNT(stor_id) FROM sales);
```

### *s)* Nome dos títulos que nunca foram vendidos na loja “Bookbeat”;

```
SELECT title FROM titles
EXCEPT
SELECT DISTINCT title FROM titlesINNER JOIN sales ON sales.title_id=titles.title_id
INNER JOIN stores ON stores.stor_id=sales.stor_id
WHERE stor_name='Bookbeat'	
```

### *t)* Para cada editora, a lista de todas as lojas que nunca venderam títulos dessa editora; 

```
... Write here your answer ...
```

## Problema 6.2

### ​5.1

#### a) SQL DDL Script
 
[a) SQL DDL File](ex_6_2_1_ddl.sql "SQLFileQuestion")

#### b) Data Insertion Script

[b) SQL Data Insertion File](ex_6_2_1_data.sql "SQLFileQuestion")

#### c) Queries

##### *a)*

```
SELECT Pname, Ssn, Fname, Lname FROM Company.Project
INNER JOIN Company.Works_on ON Pno=Pnumber
INNER JOIN Company.Employee ON Essn=Ssn;

```

##### *b)* 

```
SELECT funcionario.Fname, funcionario.Minit, funcionario.Lname
FROM Company.Employee AS funcionario, Company.Employee AS super
WHERE funcionario.Super_ssn = super.Ssn AND super.Fname='Carlos' AND super.Minit='D' AND super.Lname='Gomes'
```

##### *c)* 

```
SELECT Project.Pname, SUM(Works_on.Hours) AS horas_semana
FROM Company.Project, Company.Works_on	
WHERE Works_on.Pno = Project.Pnumber
GROUP BY Project.Pname

```

##### *d)* 

```
SELECT Fname, Lname
FROM ((Company.Works_on JOIN Company.Project ON Pno=Pnumber) JOIN Company.Employee ON Ssn=Essn)
WHERE Dno = 3 AND Pname = 'Aveiro Digital' AND Hours > 20
```

##### *e)* 

```
SELECT Fname, Lname
FROM Company.Employee LEFT OUTER JOIN Company.Works_on ON Ssn=Essn
WHERE Essn IS NULL
```

##### *f)* 

```
SELECT Dname, AVG(Salary) AS salario_medio
FROM Company.Employee JOIN Company.Department ON Dno=Dnumber
WHERE Sex='F'
GROUP BY Dname

```

##### *g)* 

```
SELECT Fname, Lname, COUNT(Dependent_name) AS Number
FROM Company.Employee JOIN Company.Dependent ON Ssn=Essn
GROUP BY Fname, Lname
HAVING COUNT(Dependent_name) > 2;

```

##### *h)* 

```
SELECT Fname, Lname
FROM Company.Employee JOIN Company.Department ON Ssn=Mgr_Ssn LEFT JOIN Company.Dependent ON Ssn=Essn
WHERE Essn IS NULL

```

##### *i)* 

```
SELECT DISTINCT Fname, Lname, [Address]
FROM Company.Employee JOIN Company.Works_on ON Ssn=Essn JOIN Company.Project ON Pno=Pnumber JOIN Company.Dept_locations ON Dno=Dnumber
WHERE Plocation='Aveiro' AND Dlocation!='Aveiro'
```

### 5.2

#### a) SQL DDL Script
 
[a) SQL DDL File](ex_6_2_2_ddl.sql "SQLFileQuestion")

#### b) Data Insertion Script

[b) SQL Data Insertion File](ex_6_2_2_data.sql "SQLFileQuestion")

#### c) Queries

##### *a)*

```
select fornecedor.nome
from StockManagement.fornecedor
left join StockManagement.encomenda on fornecedor.nif = encomenda.fornecedor
where encomenda.fornecedor is null;
```

##### *b)* 

```
select codProd, avg(unidades) as media
from StockManagement.item
group by codProd
```


##### *c)* 

```
SELECT AVG(numero_produtos) AS media_produtos_por_encomenda
FROM (
    SELECT numEnc, COUNT(codProd) AS numero_produtos
    FROM item
    GROUP BY numEnc
) as subquery;
```


##### *d)* 

```
select fornecedor.nome, produto.nome, sum(item.unidades) as quantidade
from fornecedor join encomenda on nif=fornecedor join item on numEnc=numero join produto on codigo = codProd
group by fornecedor.nome, produto.nome
order BY fornecedor.nome
```

### 5.3

#### a) SQL DDL Script
 
[a) SQL DDL File](ex_6_2_3_ddl.sql "SQLFileQuestion")

#### b) Data Insertion Script

[b) SQL Data Insertion File](ex_6_2_3_data.sql "SQLFileQuestion")

#### c) Queries

##### *a)*
##### *a)*

```
select paciente.nome, paciente.numUtente from prescricao 
full outer join paciente on prescricao.numUtente=paciente.numUtente
where numPresc is null
```

##### *b)* 

```
select m.especialidade, count(p.numpresc) as total_presc 
from prescricao p
inner join medico m on p.nummedico = m.numsns
group by m.especialidade
```

##### *c)* 

```
select farmacia.nome, count(prescricao.numPresc) as total_presc
from prescricao.prescricao
inner join farmacia on prescricao.farmacia = farmacia.nome
group by prescricao.farmacia.nome;

```

##### *d)* 

```
select farmaco.nome
from farmaco 
where farmaco.numregfarm = 906 
EXCEPT
select presc_farmaco.nomefarmaco
from prescricao 
left outer join presc_farmaco on prescricao.numpresc = presc_farmaco.numpresc
where presc_farmaco.numregfarm = 906;
```

##### *e)* 

```
select farmacia,nome,count(nomeFarmaco) as farmaco_vendido_farmacia from prescricao,presc_farmaco,farmaceutica
where prescricao.dataProc is not null and presc_farmaco.numPresc = prescricao.numPresc and numReg=numRegFarm
group by farmacia,nome
order by farmacia


```

##### *f)* 

```
SELECT nome FROM paciente
INNER JOIN (
			SELECT numUtente, COUNT(numMedico) AS medicos_dif FROM 
				( SELECT numUtente, numMedico FROM prescricao ) AS AUX
			GROUP BY numUtente
			HAVING (COUNT(numMedico)>1)
			) AS UTENTE_2MED
ON paciente.numUtente=UTENTE_2MED.numUtente
```
