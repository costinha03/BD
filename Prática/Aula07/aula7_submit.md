# BD: Guião 7


## ​7.2 
 
### *a)*

```
A relação apresentada encontra-na 1FN, uma vez que os seus atributos são atómicos e não permite relações dentro de outras.
```

### *b)* 

```
2FN 
Livro(A, D, E, F, G, H, I)
Autor(B, C)

3FN
    R1 (A, B, G, F, I)
    R2 (B,C)
    R3 (D,F,E)
    R4 (G,H)
```




## ​7.3
 
### *a)*

```
{A,B}
```


### *b)* 

```
R1(A, B, C)
R2(A, D, E, I, J)
R3(B, F, G, H)
```


### *c)* 

```
R1(A, B, C) --> DF1
R2(A, D, E) --> DF2
R3(B, F) --> DF3
R4(D, I, K) --> DF5
R5(F, G, H) --> DF4
```




## ​7.4
 
### *a)*

```
{A, B}
```


### *b)* 

```
R1(A, B, C, D) R2(D, E)
```


### *c)* 

```
R1(A,B,C,D) 
R2(D, E) 
R3(C, A)
```



## ​7.5
 
### *a)*

```
{A,B}
```

### *b)* 

```
R1(_A_,_B_,D,E) DF1
R2(_A_,C, D) DF2 e DF3

```


### *c)* 

```
R1(_A_,_B_,D,E) DF1
R2(_A_,C) DF2
R3(_C_,D) DF3

```

### *d)* 

```
Igual à anterior, visto que em todas as relações, todos os atributos dependem da totalidade da chave
R1(_A_,_B_,D,E) DF1
R2(_A_,C) DF2
R3(_C_,D) DF3
```
