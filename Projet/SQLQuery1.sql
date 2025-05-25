/*DECLARE @UserID INT;

-- Insérer l'utilisateur
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('Directeur', 'Informatique', 'directeur1@gmail.com', 'motdepasse123', 'Directeur');

-- Récupérer l'ID de l'utilisateur inséré
SET @UserID = SCOPE_IDENTITY();

-- Insérer dans DirecteurInformatique
INSERT INTO [dbo].[DirecteurInformatique] (id)
VALUES (@UserID);
*/
/*
Select * from Utilisateur;
select * from DirecteurInformatique;
*/
/*
DECLARE @UserID INT;

-- Insérer l'utilisateur
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('chef', 'projet', 'chefprojet1@gmail.com', 'motdepasse123', 'Chef de Projet');

-- Récupérer l'ID généré
SET @UserID = SCOPE_IDENTITY();

-- Insérer dans ChefProjet
INSERT INTO [dbo].[ChefProjet] (id)
VALUES (@UserID);
*/
/*
Select * from Utilisateur;
select * from DirecteurInformatique;
*/
/*
DECLARE @nomProjet NVARCHAR(150) = 'Projet E-commerce4';
DECLARE @description NVARCHAR(MAX) = 'Développement d’un site e-commerce avec gestion de produits, commandes et paiements.';
DECLARE @client NVARCHAR(150) = 'ClientTech2';
DECLARE @dateDemarrage DATE = '2025-06-01';
DECLARE @dateLivraison DATE = '2025-08-01';
DECLARE @nombreJoursDev INT = 45;
DECLARE @directeurId INT = 1; -- Remplacez par un ID existant dans DirecteurInformatique

-- Vérifier l'unicité du nom du projet
IF NOT EXISTS (SELECT 1 FROM [dbo].[Projet] WHERE [nom] = @nomProjet)
BEGIN
    INSERT INTO [dbo].[Projet] (
        nom, description, client, dateDemarrage, dateLivraison, nombreJoursDev, directeurId
    )
    VALUES (
        @nomProjet, @description, @client, @dateDemarrage, @dateLivraison, @nombreJoursDev, @directeurId
    );
END
ELSE
BEGIN
    RAISERROR('Le nom du projet existe déjà.', 16, 1);
END;
*/
/*
-- Utilisateur 1
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('Ali', 'Ben', 'ali.ben@email.com', 'pwd123', 'Développeur');

-- Utilisateur 2
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('Sara', 'Khelifi', 'sara.khelifi@email.com', 'pwd456', 'Développeur');

-- Utilisateur 3
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('Yassine', 'Hajji', 'yassine.hajji@email.com', 'pwd789', 'Développeur');

-- Ali Ben
INSERT INTO [dbo].[Developpeur] (id, projetId, technonlogies)
VALUES (3, 4, 'Net,React');

-- Sara Khelifi
INSERT INTO [dbo].[Developpeur] (id, projetId, technonlogies)
VALUES (4, 4, 'React,SQL');

-- Yassine Hajji
INSERT INTO [dbo].[Developpeur] (id, projetId, technonlogies)
VALUES (5, 4, 'Net,Azure');
*/
/*
INSERT INTO [dbo].[Service] (nom, descriptionService, dureeJours, developpeurAssigneId, projetId)
VALUES 
('Catalogue Produit', 'Développement du module catalogue', 10, 3, 7),
('Gestion des Commandes', 'Module de traitement des commandes clients', 15, 4, 7),
('Intégration Paiement', 'Connexion avec API Stripe et Cash on Delivery', 8, 3, 7),
('Tableau de bord', 'Interface admin pour gestion complète du site', 12, 5, 7); -- Service sans dev assigné
*/
/*
-- Service 1: Catalogue Produit
INSERT INTO Tache (nom, descriptionTache, pourcentageAvancement, developpeurId, serviceId)
VALUES 
('Analyse Catalogue', 'Analyse des besoins du module catalogue', 0.0, 3, 10),
('Développement Front Catalogue', 'Développement de linterface utilisateur pour le catalogue', 0.0, 3, 10),
('Développement Back Catalogue', 'Implémentation de la logique backend pour le catalogue', 0.0, 4, 10);

-- Service 2: Gestion des Commandes
INSERT INTO Tache (nom, descriptionTache, pourcentageAvancement, developpeurId, serviceId)
VALUES 
('Création Commande', 'Développement de la fonctionnalité de création de commande', 0.0, 4, 11),
('Historique Commandes', 'Affichage de lhistorique des commandes pour les utilisateurs', 0.0, 4, 11),
('Mise à jour Commande', 'Fonction de mise à jour de statut des commandes', 0.0, 4, 11);

-- Service 3: Intégration Paiement
INSERT INTO Tache (nom, descriptionTache, pourcentageAvancement, developpeurId, serviceId)
VALUES 
('Intégration Stripe', 'Connexion avec lAPI Stripe pour paiements en ligne', 0.0, 3, 12),
('Paiement à la livraison', 'Ajout de la méthode de paiement Cash on Delivery', 0.0, 4, 12),
('Sécurité Paiement', 'Vérification de la sécurité des transactions', 0.0, 4, 12);

-- Service 4: Tableau de bord
INSERT INTO Tache (nom, descriptionTache, pourcentageAvancement, developpeurId, serviceId)
VALUES 
('Vue Statistiques', 'Affichage des statistiques du site pour les admins', 0.0, 5, 13),
('Gestion Utilisateurs', 'Interface pour gestion des comptes utilisateurs', 0.0, 5, 13),
('Interface Réactive', 'Adaptation du tableau de bord aux différents écrans', 0.0, 4, 13);
*/
/*
-- Utilisateur 4
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('Imane', 'Boukadida', 'imane.boukadida@email.com', 'pwd000', 'Développeur');

-- Utilisateur 5
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('Rachid', 'El Amrani', 'rachid.elamrani@email.com', 'pwd111', 'Développeur');

-- Utilisateur 6
INSERT INTO [dbo].[Utilisateur] (nom, prenom, email, motDePasse, role)
VALUES ('Nour', 'Zerhouni', 'nour.zerhouni@email.com', 'pwd222', 'Développeur');

-- Imane Boukadida
INSERT INTO [dbo].[Developpeur] (id, projetId, technonlogies)
VALUES (6, NULL, 'Java,Spring');

-- Rachid El Amrani
INSERT INTO [dbo].[Developpeur] (id, projetId, technonlogies)
VALUES (7, NULL, 'Python,Django');

-- Nour Zerhouni
INSERT INTO [dbo].[Developpeur] (id, projetId, technonlogies)
VALUES (8, NULL, 'Angular,Node.js');
*/
select * from Projet;
Select * from Utilisateur;
select * from Developpeur;
Select * from Service;
select * from Tache;
select * from Notification;
/*DELETE FROM Projet WHERE id IN (1, 2);*/