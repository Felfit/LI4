
#API

##Receita

###GET
* api/Receita
  * Retorna as informações respetivas a todas as receitas
* api/Receita/{id}
  * Retorna a receita com o id com todas as informaçoes.
    * Informação
    * Ingredientes
    * Utensilios
    * Passo
      * Info
      * Ingredientes
      * Utensilios
* api/Receita/recomendadas/{idUtilizador}
  * Retorna as informações de receitas tendo em consideração os gostos do utilizador exceto as receitas das restrições alimentares
    * Nao retorna as favoritas
* api/Receita/favoritas/{idUtilizador}
  * Retorna as receitas favoritas do utilizador


##Utilizador

###GET
* api/utilizador/{id}/favoritas
  * Retorna as receitas favoritas do utilizador
* api/Utilizador/{id}
  * Retorna as informações do utilizador.

###POST
* api/utilizador
  * TO BE EXPLAINED
* api/Utilizador/autenticar
  * TO BE EXPLAINED


##Duvida

###GET
* api/duvida/{idReceita}/{idPasso}
  * Retorna array com todas as dúvidas associadas passo.