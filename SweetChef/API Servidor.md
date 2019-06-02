
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
* api/Receita/recomendadas
  * Retorna as informações de receitas tendo em consideração os gostos do utilizador exceto as receitas das restrições alimentares
    * Nao retorna as favoritas  
* api/Receita/filtradas?dif=...&dur=...&tags=...&tags
  * Retorna receitas filtradas. Todos os campos são opcionais. Tags é uma lista.
* api/Receita/favoritas/{idUtilizador}
  * Retorna as receitas favoritas do utilizador
* api/Receita/opinioes/{idReceita}
  * Retorna Média das opiniões sobre a receita
* api/Receita/filtradasTodas
  * Retorna todas as receitas exceto os dislikes e as blacklist
* api/Receita/descoberta
  * Retorna todas receitas recomendadas ao utilizador baseadas nas receitas previamente executadas 
* api/Receita/tendencias
  * Retorna todas as receitas filtradas pela configuração ordenadas descendentemente pelo numero de execuções

##Tags
###Get
* api/tags
    * Retorna lista com tags



##Utilizador

###GET
* api/Utilizador/favoritas
  * Retorna as receitas favoritas do utilizador
* api/Utilizador/
  * Retorna as informações do utilizador.
* api/Utilizador/opiniao/{idReceita}
  * Retorna a opiniao correspondente a uma receita 
* api/Utilizador/passoFeedback/{idReceita}/{idPasso}/
  * Retorna o feedback do utilizador relativo a um passo
* api/Utilizador/receitasExecutadas
  * Retorna as receitas executadas pelo utilizador, número de vezes que foram feitas e data da ultima execução, tempo de preparação e duração total mínima. Tudo ordenado por data
* api/Utilizador/estatisticas/temposMédios
  * Retorna Receitas executadas com os tempos médios de execução do utilizador
* api/Utilizador/ingredientesUsados
  * Retorna lista de Ingredientes usados com número de vezes e quantidade utilizada. Tudo ordenado decrescentemente pelo nº de utilização
* api/Utilizador/tagsUsadas
  * Retorna lista de tags usados com número de vezes que foi utilizada, ordenadas decrescentemente 
* api/Utilizador/listaCompras
  * Retorna a lista de Ingredientes necessários para os próximos 7 dias da semana
* api/Utilizador/closestStore/{lat},{lon}
  * Retorna as coordenadas da loja mais próxima
* api/Utilizador/ementa?idUt=...&dataInicial=...&dataFinal=...
  * Retorna todas as receitas dentro do intervalo e associadas a elas a data. Tudo ordenado crescentemente


###POST
* api/utilizador
  * TO BE EXPLAINED
* api/Utilizador/autenticar
  * TO BE EXPLAINED
* api/Utilizador/passoFeedback/{idReceita}/{idPasso}/?argumentos
    * string comentario
* api/Utilizador/ementa
    * Passa no form -> idUt, idRec, data 
    * Adiciona uma receita à ementa semanal
* api/Utilizador/restricoesAlimentar
    * Passa no form idUt, idReceita, data, duracao
    * Define as restrições elementares

###PUT
* api/Utilizador/opiniao/{idReceita}/?arguments
  * Chamar put para atualizar um campo. Precisa existir
    * (Optional) bool favorito
    * (Optional) short rating
    * (Optional) bool blacklisted
* api/Utilizador/passoFeedback/{idReceita}/{idPasso}/?argumentos
    * (Opcional) string comentario 
* api/Utilizador/configuracao
    * Passar no form -> idUt, List<int> restricoes, List<int> likes, List<int> dislikes
    * Definir a configuração do utilizador

###DELETE
* api/Utilizador/ementa
  * Passa no form ->  idUt, idReceita, data
  * Remove da ementa semanal uma receita


##Duvida

###GET
* api/duvida/{idReceita}/{idPasso}
  * Retorna array com todas as dúvidas associadas passo.

##Ingrediente

###GET
* api/Ingrediente
  * Retorna uma lista com todos os ingredientes