ProActive Windows Agent 2.2

La solution comporte 5 projets :

Agent FirstSetup
======================
Permert de définir les paramètres à l'installation via la GUI.
Contient la classe "SvrInstaller" qui se charge d'installer le service


AgentForAgent
======================
Il s'agit de l'interface graphique permettant de
- Modifier le fichier de configuration xml
- Démarrer/Arreter le service
- Voir les logs


ConfigParser
======================
Relatif au fichier de configuration, on y retrouve les classes en rapport avec les actions, les events


ProActiveAgent
======================
Ce projet correspond au service qui est installé. La classe WindowsService lit la configuration et démarre/arrete les runtimes.
On y retrouve également les surcharges des méthodes OnStart(), OnStop(), ...


ScreenSaver
======================
L'écran de veille ProActive qui correspond à nu évènement de démarrage de runtime

Added this line just to test something
