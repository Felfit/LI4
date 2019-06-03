INSERT INTO Receita
	(imagemLink, videoLink, descricao, nome, dificuldade, porcoes, tempodepreparacao, tempodeespera, energia, gordura, hidratosCarbono)
	VALUES 
	('https://www.saborintenso.com/attachments/videos-doces/377d1252681021-bolo-bolacha-bolo-bolacha-3.jpg', '/videos/boloBolacha/ReceitaToda.mp4', 'Ninguém resiste a um bolo de bolacha cremoso, venha daí e prepare a nossa receita de bolo de bolacha simples e fácil de fazer. Se for dia de festa, este bolo vai fazer sucesso. Experimente e tenha um dia em grande!', 'Bolo de Bolacha', 2, 12, 40, 120, 350, 25, 40);

INSERT INTO Unidade
	(nome)
	VALUES
	('kilogramas'),
	('gramas'),
	('litros'),
	('decilitros'),
	('mililitros'),
	('unidade'),
	('unidades');

INSERT INTO Ingrediente
	(Unidadeid, nome, imageLink)
	VALUES
	(7, 'bolacha maria', 'https://images.vidaativa.pt/articles/850_400_bolachas-maria_1516979484.jpg'),
	(2, 'manteiga', 'https://www.independent.ie/business/farming/article36475185.ece/ALTERNATES/h342/FIN_2013-12-19_BUS_007_30031725_I1.JPG'),
	(2, 'açúcar fino', 'https://images.immediate.co.uk/production/volatile/sites/4/2018/08/iStock_63854727_LARGE-a43c35e.jpg?quality=45&resize=960,413'),
	(7, 'ovo', 'https://upload.wikimedia.org/wikipedia/commons/7/7b/Egg_upright.jpg'),
	(5, 'café' , 'https://www.gannett-cdn.com/presto/2018/11/21/USAT/55c6954d-2d02-4c41-a61f-b7d302d32658-YL_FLAG_coffee.JPG?width=534&height=712&fit=bounds&auto=webp'),
	(7, 'grão de café', 'https://5.imimg.com/data5/XK/JC/MY-12108908/coffee-beans-750-to-850-kg-500x500.jpg'),
	(2, 'iogurte', 'http://3.bp.blogspot.com/_lW5crvJoV4s/Se9-eq3Lj5I/AAAAAAAAAMY/BDDWnXQNtOQ/s400/DSC04372.JPG');

INSERT INTO Utensilio
	(nome, imageLink)
	VALUES
	('batedeira', 'https://www.casasbahia-imagens.com.br/Eletroportateis/Batedeiras/batedeira/1736253/6593614/batedeira-philips-walita-viva-collection-ri7200-400w-1736252.jpg'),
	('espátula' , 'https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Kitchen-spatula.jpg/1200px-Kitchen-spatula.jpg'),
	('faca de barrar', 'https://online.e-leclerc.pt/imgs/produtos/TUbIZVnM8kUvpMFdq3DRKZ5d.jpg'),
	('recipiente para líquidos', 'https://img.elo7.com.br/product/zoom/1A23300/tijela-branca-japonesa-sopa.jpg'),
	('suporte para bolos', 'https://cdn.awsli.com.br/600x450/41/41975/produto/4253582/8d55236ff9.jpg'),
	('frigorífico', 'https://www.ikea.com/pt/pt/images/products/lagan-frigorifico-congelador-a-branco__0280593_PE419391_S4.JPG');


INSERT INTO Receita_Ingrediente
	(quantidade, Receitaid, Ingredienteid)
	VALUES
	(44, 1, 1),
	(250, 1, 2),
	(250, 1, 3),
	(3, 1, 4),
	(500, 1, 5),
	(20, 1 , 6);

INSERT INTO Utensilio_Receita
	(Receitaid, Utensilioid)
	VALUES
	(1,1),
	(1,2),
	(1,3),
	(1,4),
	(1,5);


INSERT INTO Passo 
	(numero, Receitaid, duracao, descricao, imagemLink, videoLink, linkExterno)
	VALUES
	(1, 1, 5, 'Bata a manteiga até ficar um creme branco', '/images/boloDeBolacha/bater.PNG', '/videos/boloBolacha/passo1.mp4', null),
	(2, 1, 2, 'Junte açúcar ao creme e continue a bater', '/images/boloDeBolacha/beterComAcucar.PNG', '/videos/boloBolacha/passo2.mp4', null),
	(3, 1, 3, 'Junte as gemas de ovos uma de cada vez ao creme enquanto continua a bater até ficar homogéneo', '/images/boloDeBolacha/baterComOvos.PNG', '/videos/boloBolacha/passo3.mp4', null),
	(4, 1, 2, 'Passe 6 bolachas maria por café, uma de cada vez', '/images/boloDeBolacha/molhar.PNG', '/videos/boloBolacha/passo4.mp4', null),
	(5, 1, 2, 'Coloque as bolachas no centro do suporte para bolos e barre o topo com o creme', '/images/boloDeBolacha/semiMonte.PNG', null, null),
	(6, 1, 20, 'Repita os dois passos anteriores, para formar as várias camadas do bolo até acabarem as bolachas', '/images/boloDeBolacha/monte.PNG', '/videos/boloBolacha/passo6.mp4', null),
	(7, 1, 4, 'Cubra o bolo com o creme restante', '/images/boloDeBolacha/quaseBom.PNG', '/videos/boloBolacha/passo7.mp4', null),
	(8, 1, 2, 'Decore o resultado com a bolacha ralada e com os grãos de café', 'https://www.saborintenso.com/attachments/videos-doces/377d1252681021-bolo-bolacha-bolo-bolacha-3.jpg', '/videos/boloBolacha/', null),
	(9, 1, 120, 'Leve ao frigorifico durante 2 horas até ficar fresco','https://www.saborintenso.com/attachments/videos-doces/377d1252681021-bolo-bolacha-bolo-bolacha-3.jpg', null, null);

INSERT INTO Duvida
	(titulo, videoLink, imagemLink, linkexterno, Explicacao)
	VALUES
	('Gema', null, 'https://sitedoovo.com.br/wp-content/uploads/2019/02/gema-do-ovo.jpg', null, 'A gema está situada centralmente no interior do ovo e possui uma forma esférica. É nela que se concentra a parte mais gordurosa e calórica do ovo'),
	('Clara', null, 'https://www.mundoboaforma.com.br/wp-content/uploads/2015/07/Eggwhite-614x330.jpg', null, 'A clara trata-se de uma substância aquosa e viscosa, praticamente transparente e sem odor próprio. A clara é rica em proteínas e é de alta digestão.'),
	('Separar gema da clara', 'https://www.youtube.com/watch?v=tx72s86URhA', null, null, null),
	('Mistura homogénea', null, null, null, 'Misturas homogêneas são aquelas em que não se consegue perceber a diferença entre duas ou mais substâncias. Exemplos destas misturas são a água salgada e o ar'),
	('Barrar', null, 'https://4men.pt/wp-content/uploads/2019/01/0f95b97c47b9affa49e5bb97f55d5cbc-754x394.jpg', null, 'Cobrir, normalmente, um objeto sólido com algum tipo de mistura pegajosa'),
	('Bolacha ralada', 'https://www.youtube.com/embed/OD9Rp-UJNG4?start=12&end=25', null, null, null),
	('Alternativo a grãos de café', null, 'https://st.depositphotos.com/3695467/5158/i/950/depositphotos_51584001-stock-photo-grated-chocolate.jpg', null, 'Como alternativa aos grãos de café ou até em conjunto pode ser utilizado chocolate ralado que também ficará delicioso.'),
	('Levar ao frigorifico', null, null, null, 'É necessário colocar o bolo no frigorífico, isto para lhe conferir consistência. De outro modo o bolo iria-se desfazer facilmente');

INSERT INTO Passo_Dúvida
	(Passoid, PassoReceitaid, Dúvidaid, questao)
	VALUES
	(3, 1, 1, 'Que parte do ovo é a gema?'),
	(3, 1, 2, 'O o que é a clara do ovo?'),
	(3, 1, 3, 'Como é que posso separar a clara da gema?'),
	(3, 1, 4, 'O que é uma mistura Homogénea?'),
	(5, 1, 5, 'Como é que se barra algo?'),
	(8, 1, 6, 'Como é que posso partir bolachas?'),
	(8, 1, 7, 'Quer uma alternativa ao grão de café'),
	(9, 1, 8, 'Porque levar ao frigorífico?');

INSERT INTO Passo_Ingrediente
	(quantidade, Passoid, PassoReceitaid, Ingredienteid)
	VALUES
	(250, 1, 1, 2),
	(250, 2, 1, 3),
	(3, 3, 1, 4),
	(6, 4, 1, 1),
	(6, 5, 1, 1),
	(30, 6, 1, 1),
	(8, 8, 1, 1),
	(20, 8, 1, 6);

INSERT INTO Utensilio_Passo
	(Passoid, PassoReceitaid, Utensilioid)
	VALUES
	(1, 1, 1),
	(2, 1, 1),
	(3, 1, 1),
	(4, 1, 2),
	(4, 1, 4),
	(5, 1, 3),
	(5, 1, 5),
	(6, 1, 3),
	(6, 1, 5),
	(7, 1, 3),
	(7, 1, 5),
	(8, 1, 5),
	(9, 1, 6);


INSERT INTO Tag
	(tag)
	VALUES
	('sem gluten'),
	('sem lactose'),
	('sem açúcar adicionado'),
	('vegetariano'),
	('vegan'),
	('bolo'),
	('semi-frio'),
	('gelado'),
	('pudim'),
	('tarte');

INSERT INTO Tag_Receita
	(Receitaid, Tagid)
	VALUES
	(1,3),
	(1,6),
	(1,7);
	