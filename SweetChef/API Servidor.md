
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
* api/Receita/filtradas?dif=...&dur=...&tags=...&tags
  * Retorna receitas filtradas. Todos os campos são opcionais. Tags é uma lista.
* api/Receita/favoritas/{idUtilizador}
  * Retorna as receitas favoritas do utilizador


##Utilizador

###GET
* api/utilizador/{id}/favoritas
  * Retorna as receitas favoritas do utilizador
* api/Utilizador/{id}
  * Retorna as informações do utilizador.
* api/Utilizador/{idUt}/opiniao/{idReceita}
  * Retorna a opiniao correspondente a uma receita 
* api/Utilizador/{idUt}/passoFeedback/{idReceita}/{idPasso}/
  * Retorna o feedback do utilizador relativo a um passo
* api/Utilizador/{idUt}/receitasExecutadas
  * Retorna as receitas executadas pelo utilizador, número de vezes que foram feitas e data da ultima execução 
* api/Utilizador/{idUt}/estatisticas/temposMédios
  * Retorna Receitas executadas com os tempos médios de execução do utilizador
* api/Utilizador/{idUt}/ingredientesUsados
  * Retorna lista de Ingredientes usados com número de vezes e quantidade utilizada
* api/Utilizador/{idUt}/listaCompras
  * Retorna a lista de Ingredientes necessários para os próximos 7 dias da semana
* api/Utilizador/closestStore/{lat},{lon}
  * Retorna as coordenadas da loja mais próxima


###POST
* api/utilizador
  * TO BE EXPLAINED
* api/Utilizador/autenticar
  * TO BE EXPLAINED
* api/Utilizador/{idUt}/opiniao/{idReceita}/?arguments
  * Chamar Put para criar opinião. se não existir
    * bool favorito
    * (Optional) short rating
    * bool blacklisted
* api/Utilizador/{idUt}/passoFeedback/{idReceita}/{idPasso}/?argumentos
    * string comentario 

###PUT
* api/Utilizador/{idUt}/opiniao/{idReceita}/?arguments
  * Chamar Post para atualizar um campo. Precisa de existir
    * (Optional) bool favorito
    * (Optional) short rating
    * (Optional) bool blacklisted
* api/Utilizador/{idUt}/passoFeedback/{idReceita}/{idPasso}/?argumentos
    * (Opcional) string comentario 
##Duvida

###GET
* api/duvida/{idReceita}/{idPasso}
  * Retorna array com todas as dúvidas associadas passo.