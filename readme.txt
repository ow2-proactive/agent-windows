ProActive Windows Agent 2.2

La solution inclut 5 projets :

Agent FirstSetup
======================
Permert de d�finir les param�tres � l'installation via la GUI.
Contient la classe "SvrInstaller" qui se charge d'installer le service


AgentForAgent
======================
Il s'agit de l'interface graphique permettant de
- Modifier le fichier de configuration xml
- D�marrer/Arreter le service
- Voir les logs


ConfigParser
======================
Relatif au fichier de configuration, on y retrouve les classes en rapport avec les actions, les events


ProActiveAgent
======================
Ce projet correspond au service qui est install�. La classe WindowsService lit la configuration et d�marre/arrete les runtimes.
On y retrouve �galement les surcharges des m�thodes OnStart(), OnStop(), ...


ScreenSaver
======================
L'�cran de veille ProActive qui correspond � nu �v�nement de d�marrage de runtime