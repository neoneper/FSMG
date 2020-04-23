# FSMG Last Update at Unity 2019.3.9f1
https://www.youtube.com/watch?v=WlDU9wm8t6c&t=28s

Doc: https://github.com/neoneper/FSMG/wiki

Discord: https://discord.gg/GV5mA3t

Examples: https://github.com/neoneper/FSMGExamples

###Visão Geral:
Segue abeixo algumas informações úteis sobre o FSMG. Se você não entende o que é ou como o FSMG pode te ajudar, aconselho que continue a leitura.
 
 - FSMG (Finite State Machine Graph), é uma framework para árvores de comportamento baseadas em nós, destinada a trabalhar como máquina de estado finita. 
 - FSMG não é uma ferramenta de visual scripting, muito menos um processador de script em tempo real.
 - FSMG não é destinada a um tipo especifico de jogo e por padrão não vem com nenhum jogo pronto. Para exemplos de uso do FSMG acesse o repositório de exemplos.
 - Suportado por .Net2x e 4X
 - FSMG foi projetado utilizando o framework XNODE.
 - Nao há necessidade de conhecimento prévio do XNODE para utilizar o FSMG. 
 - FSMG não segue um padrão UML de fluxo. Ele foi projetado para ser compacto e eficiênte.
 - FSMG trabalha com um unico estado por vez.
 - FSMG espera que você crie suas proprias ações e decisões baseadas na arquitetura adotada em seu jogo. O grafico foi projetado para aceitar como entrada ScriptableObjects com funções de ação e decisão, que você simplesmente Pluga em nós especificos do gráfico. Assim sendo o unico conhecimento previo que o FSMG espera de você é o basico sobre ScriptableObjecs e logicamente conhecimento da sua própria implemntação de IA.
  - Nós de conexão e linhas de coneções são utilizados para ligar os nós do gráfico e indiciar o fluxo de transição dos estados. O gráfico considera as cores das conexões assim sendo você não podera conectar um bloco de nó AZUL em um bloco de nó vermelho. Por padrão o sistema randomiza essas cores na primeira vez que você o utilizar, você pode REDEFINIR estas cores entrando nas preferencias do gráfico em (Edit->Preferences->XnodeEditor).
 - Formato de linhas de conexões. Voce pode mudar o estilo das linhas mostradas no grafico acessando as preferencias do Grafico em :
(Edit->Preferences->XnodeEditor).
  - Você não precisa criar novos nós para o grafico, dificilmente haverá necessidade porém você pode. O Gráfico foi projetado para facilitar a criação de novos nós atraves de classes base que ja trazem todo o backend de funcionamento pronto.
  - AI_ActionBase: É a classe base (scriptableObject) da qual todos os assets de ações que você fizer precisam derivar. Elas serão anexadas a nós de especificos no grafico e serã uma ponte direta entre o fluxo de estados do grafico e seu componente FSM(IA, Agente).
  - AI_DecisionBase: É a classe base (scriptableObject), da qual todas os assets de decisões que você implementar precisam derivar.
  Eles serão anexados a nós especificos do gráfico e também servem como uma ponte direta entre os estados do seu grafico e seu componente FSM(AI, Agente...)...
  - FSMBehaviour: É a classe base (Monobehaviour), da qual seus controladores de IA (Agentes) devem derivar. Você pode querer implementar agentes utilizando o NavMeshAgent, nativo da unity, ou A*, ou qualquer outro tipo de controlador.
  - FSMBehaviour traz todo o backend de comunicação com o gráfico, bem como o sistema de variaveis e trajetos, assim sendo ao implementa-lo em sua classe de controle de IA, você so vai precisar se preocupar com sua IA.
  - FSMBehaviour não sobrepoe nenhuma funcão padrão da Monobehaviour, tais como Awake, Start, Update etc... deixando-as livres para você.
 
 It is still in the testing phase.

- Allows multiple IAs per Graph.
- Creation of local and global variables directly on the graph.
- Creation of direct local and global routes on the graph.
- 100% PlugAndPlay. You can create new states or just new ones.
- All categories of actions (Actions, Decisions and States) have an "API", which allows the user to create a custom resource for them, in case the user does not want to create new blocks.
-------------------------------------------
FSMG reports still being born, i spended a lot of time developing and there i stilled no active time to document or to work with real use cases.

Anyway. It is open to anyone who wants to contribute or use only. User comments are welcome.
Fell free to use and to help improve.

PS: To work with FSGM you need download XNODE Pack. 
https://github.com/neoneper/xNode
