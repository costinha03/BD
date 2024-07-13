# BD: Guião 5


## ​Problema 5.1
 
### *a)*

```
π Pname, Ssn,Fname,Lname, Minit (employee ⨝ Essn=Ssn (works_on) ⨝ Pno=Pnumber (project))
```


### *b)* 

```
temp = ρ ESuper π Ssn (σ Fname = 'Carlos' ∧ Minit = 'D' ∧ Lname = 'Gomes' (employee))
 
π Fname, Minit, Lname (temp ⨝  ESuper.Ssn = employee.Super_ssn employee)
```


### *c)* 

```
γ Pnumber,Pname;sum(Hours)->horas (employee ⨝Ssn=Essn (project ⨝Pnumber=Pno works_on))
```


### *d)* 

```
π Ssn,Fname,Minit,Lname (σ Dno=3 ∧ Hours>20 (employee ⨝ Ssn=Essn works_on))
```


### *e)* 

```
π Ssn,Fname,Minit,Lname (σ Pno=null (employee ⟕Ssn=Essn works_on))
```


### *f)* 

```
funcionarios = employee ⨝Dno=Dnumber department

γ Dname;avg(Salary)->salario_medio (σ Sex='F' (funcionarios))
```


### *g)* 

```
dependentes = employee ⨝Ssn=Essn dependent

numDependentes = γ Ssn,Fname,Minit,Lname;count(Dependent_name)->dependentes (dependentes)

π Ssn,Fname,Minit,Lname (σ dependentes>2 (numDependentes))	
```


### *h)* 

```
gestores = employee ⨝ Ssn = Mgr_ssn department

π Ssn,Fname,Minit,Lname (σ dependent.Essn=null (gestores ⟕Ssn=Essn dependent))
```


### *i)* 

```
projeto_funcionario = project ⨝Pno=Pnumber (employee ⨝Ssn=Essn works_on)

local_departamento = π department.Dnumber,Dname,Dlocation (department ⨝department.Dnumber=dept_location.Dnumber dept_location)

π Ssn,Fname,Minit,Lname,Address (σ dept_location.Dlocation≠'Aveiro' ∧ project.Plocation='Aveiro' (projeto_funcionario ⨝Dno=Dnumber local_departamento))
```


## ​Problema 5.2

### *a)*

```
π nome (σ numero=null (fornecedor⟕(nif=fornecedor) encomenda))
```

### *b)* 

```
γ codProd;avg(unidades)->TotalUnidades item
```


### *c)* 

```
tot_produtos_encomenda = γ numEnc; count(numEnc)-> total_product (item)

γ avg(total_product)-> numero_medio_produtos (tot_produtos_encomenda)
```


### *d)* 

```
totalEncomenda = γ numEnc; count(numEnc)-> totalProduto (item)

γ avg(totalProduto)-> NumeroMedio (totalEncomenda)
```


## ​Problema 5.3

### *a)*

```
π numUtente,nome (σ numPresc = null (paciente ⟕ prescricao))
```

### *b)* 

```
prescricaoMedicos = medico ⨝ numSNS = numMedico prescricao
γ especialidade;count(especialidade)->NumerPrescricoes (prescricaoMedicos)
```


### *c)* 

```
farmacias = farmacia ⨝ nome = farmacia prescricao

γ nome; count(numPresc) ->PrescricoesProcessadas (farmacias)
```


### *d)* 

```
FarmacosFarmacia906 = π nome (σ numRegFarm = 906 (farmaco))

FarmacosPrescritos906 = π presc_farmaco.nomeFarmaco (σ numRegFarm=906 (presc_farmaco))

FarmacosFarmacia906-FarmacosPrescritos906
```

### *e)* 

```
Prescricaofarmacia = farmacia ⨝nome=farmacia prescricao

FarmaceuticaFarmacia = π presc_farmaco.numPresc, numRegFarm, nomeFarmaco, nome (presc_farmaco ⨝prescricao.numPresc=presc_farmaco.numPresc (Prescricaofarmacia))

nomeFarmaceutica_farmaco_farmacia = π presc_farmaco.numPresc, farmaceutica.nome, nomeFarmaco, farmacia.nome (farmaceutica ⨝numReg=numRegFarm FarmaceuticaFarmacia)

γ farmacia.nome,farmaceutica.nome;count(farmaceutica.nome)->vendas (nomeFarmaceutica_farmaco_farmacia)
```

### *f)* 

```
prescricao_paciente = prescricao ⨝prescricao.numUtente=paciente.numUtente paciente

medico_paciente = π numSNS,medico.nome,paciente.nome (medico ⨝numSNS=numMedico (prescricao_paciente))

γ paciente.nome;count(medico.nome)->num_medicos (medico_paciente)
```
