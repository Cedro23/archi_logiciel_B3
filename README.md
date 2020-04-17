# Projet d'architecture logicielle de Florian ARMENOULT et Cédric LESUEUR


## Documentation API

Les éléments suivants permettent de mettre en place l'API du projet de simulation de mise en orbite.
Cette API repose sur le framework Django et le paquet Django REST Framework

### Prérequis

-   Python 3.6+

-   PostgreSQL 10+

**L’utilisation d’un environnement virtuel est fortement recommandée.**
Le guide des environnements virtuels est disponible à cette adresse : [https://docs.python.org/3/library/venv.html](https://docs.python.org/3/library/venv.html)

### Installation de l'api en local

#### Installation des dépendances
 Les paquets python nécessaires sont présents dans le fichier `requirements.txt` et peuvent être installés avec la commande 

    pip install -r requirements.txt


#### Mise en place de la base de données

**Une base de données doit auparavant être créée sur PostgreSQL**

 Les informations relatives à la base de données sont à précisées dans le fichier `params.py` du dossier **Orbit_sim** :

    DB_NAME = "nom_de_la_base"
    DB_USER = "utilisateur_de_la_base"
    DB_PASSWORD = "mot_de_passe_de_l_utilisateur"
    DB_HOST = "localhost"
    DB_PORT = 5432  # 5432 est le port par défaut, peut etre amené à varier en fonction de l'environnement

 
 Après création de la base de données, migration des tables (application du modèle de données) avec les deux commandes suivantes : 

     python manage.py makemigrations && python manage.py makemigrations api

     python manage.py migrate && python manage.py migrate api
     
#### Lancement du serveur

Il ne reste plus qu'à lancer le serveur :

    python manage.py runserver

L'IP et le port sont indiqués dans le retour de la console, par défaut l'URL sera : [http://127.0.0.1:8000](http://127.0.0.1:8000)

## Documentation Client

Les éléments suivants permettent de mettre en place le client du projet de simulation de mise en orbite.
Le client repose sur le moteur Unity.

### Prérequis 

- Unity version 2020.1.0a25 en passant par UnityHub

- L'API

- La base de données

### Installation du projet

Il suffit d'ouvrir le dossier SimOrbit du repo Git directement dans Unity.

### Utilisation

Le projet peut être utilisé directement dans l'éditeur. Il y a également une version compilée dans le dossier "SimOrbit" > "Build".

### Liens

Django : [https://www.djangoproject.com/](https://www.djangoproject.com/) <br>
Django Rest Framework : [https://www.django-rest-framework.org/](https://www.django-rest-framework.org/) <br>
UnityHub Download : https://unity3d.com/fr/get-unity/download/

### Contacts

En cas de problèmes
Florian ARMENOULT (API-BDD) : florian.armenoult@ynov.com <br>
Cédric LESUEUR (CLIENT) : cedric.lesueur@ynov.com