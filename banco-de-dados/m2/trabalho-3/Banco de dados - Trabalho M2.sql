CREATE TABLE `usuario` (
  `id` integer PRIMARY KEY,
  `nome_usuario` varchar(255),
  `senha` varchar(255),
  `email` varchar(255)
);

CREATE TABLE `midia` (
  `id` integer PRIMARY KEY,
  `titulo` varchar(255),
  `sinopse` text,
  `data_lancamento` date,
  `plataforma` varchar(255),
  `url` varchar(255),
  `tipo_midia` varchar(255),
  `produtora_id` integer,
  `subtipo_midia_id` integer
);

CREATE TABLE `subtipo_midia` (
  `id` integer PRIMARY KEY,
  `nome_subtipo` varchar(255),
  `descricao_subtipo` text
);

CREATE TABLE `subtipo_midia_em_midia` (
  `id` integer PRIMARY KEY,
  `midia_id` integer,
  `subtipo_midia_id` integer
);

CREATE TABLE `produtora` (
  `id` integer PRIMARY KEY,
  `nome_produtora` varchar(255),
  `pais_origem` varchar(255),
  `descricao` text,
  `site_produtora` varchar(255)
);

CREATE TABLE `agendamento` (
  `id` integer PRIMARY KEY,
  `usuario_id` integer,
  `midia_id` integer,
  `data_hora_agendamento` timestamp
);

CREATE TABLE `midia_similar` (
  `id` integer PRIMARY KEY,
  `midia_principal_id` integer,
  `midia_similar_id` integer
);

ALTER TABLE `midia` ADD FOREIGN KEY (`produtora_id`) REFERENCES `produtora` (`id`);

ALTER TABLE `midia` ADD FOREIGN KEY (`subtipo_midia_id`) REFERENCES `subtipo_midia` (`id`);

ALTER TABLE `subtipo_midia_em_midia` ADD FOREIGN KEY (`midia_id`) REFERENCES `midia` (`id`);

ALTER TABLE `subtipo_midia_em_midia` ADD FOREIGN KEY (`subtipo_midia_id`) REFERENCES `subtipo_midia` (`id`);

ALTER TABLE `agendamento` ADD FOREIGN KEY (`usuario_id`) REFERENCES `usuario` (`id`);

ALTER TABLE `agendamento` ADD FOREIGN KEY (`midia_id`) REFERENCES `midia` (`id`);

ALTER TABLE `midia_similar` ADD FOREIGN KEY (`midia_principal_id`) REFERENCES `midia` (`id`);

ALTER TABLE `midia_similar` ADD FOREIGN KEY (`midia_similar_id`) REFERENCES `midia` (`id`);
