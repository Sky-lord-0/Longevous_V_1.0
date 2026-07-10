# 🎧 SDD – Sound Design Document

**Projeto:** Longevus
**Versão:** 2.0
**Status:** Em desenvolvimento

---

## 1. Finalidade do Documento

Este documento descreve a estrutura completa do design sonoro do jogo **Longevus**, cobrindo todas as trilhas musicais, sons de ambiente, efeitos sonoros de jogador, inimigos e chefe, e sons de interface. Serve como referência técnica e criativa para o sistema de áudio do projeto, documentando o caminho real dos arquivos, seus contextos de uso, eventos de acionamento e diretrizes de mixagem.

---

## 2. Visão Geral do Universo Sonoro

**Longevus** é um jogo de ação 2D em pixel art de temática **dark fantasy**, no qual o jogador controla **A Morte** — uma entidade sobrenatural que invade uma aldeia de ocultistas e avança até o castelo do líder do culto.

A proposta sonora reflete essa identidade:

- Reforçar a ambientação **sombria e mística** de cada área do jogo
- Diferenciar sonoramente a progressão entre **Vilarejo** e **Castelo**
- Comunicar com clareza as ações do jogador (pulo, dash, ataque, morte)
- Distinguir os diferentes **tipos de inimigo** e seus ataques
- Intensificar a tensão no **encontro com o chefe**
- Fornecer feedback auditivo limpo para todas as mecânicas de combate

---

## 3. Estrutura de Diretórios de Áudio

```
assets/
└── audio/
    ├── music/                          # Trilhas musicais de área e chefe
    │   ├── musicaVilarejoMUSIC.mp3
    │   ├── musicaCastelloMUSIC.mp3
    │   └── musicaBossMUSIC.mp3
    │
    ├── amb/                            # Sons de ambiente atmosférico
    │   ├── ventoLeveAMB.mp3
    │   └── ventoForteAMB.mp3      
    │
    ├── ui/                             # Áudios de interface
    │   ├── musicaTelainicialUI.mp3
    │   └── optionsButtonUI.mp3
    │
    └── sfx/                            # Efeitos sonoros de gameplay
        ├── puloSFX.mp3
        ├── efeitoDashSFX.mp3
        ├── evaporandoSFX.mp3
        ├── foiceCaindo1SFX.mp3
        ├── foiceCaindo2SFX.mp3
        ├── foiceCaindo3SFX.mp3
        ├── foiceCaindo4SFX.mp3
        ├── foiceCaindo5SFX.mp3
        ├── ataqueFoice3SFX.mp3
        ├── combateFoice1SFX.mp3
        ├── combateFoice2SFX.mp3
        ├── ataqueMelee1ANDRangerSFX.mp3
        ├── ataqueMelee2SFX.mp3
        ├── explosaoAtaqueRangerSFX.mp3
        ├── lancamentoBolaDeFogoBossSFX.mp3 
        ├── explosaoAtaqueBossCeuSFX.mp3
        ├── explosaoBolaDeFogoBossSFX.mp3
        ├── portaAbrindoSFX.mp3
        ├── portaFechandoSFX.mp3
        ├── portaAbrindoEntradaCasteloSFX.mp3
        ├── portaFechandoEntradaCasteloSFX.mp3
        ├── portaAbrindoSaidaCasteloSFX.mp3
        ├── portaFechandoSaindaCasteloSFX.mp3
        └── BloodHit/                   # Variações de impacto com sangue
            ├── bloodHit1SFX.mp3
            ├── bloodHit2SFX.mp3
            └── bloodHit3SFX.mp3
```

> **Pastas temporárias (não fazem parte da build final):**
> - `sfx/Teste/` — arquivos em avaliação
> - `sfx/Escolha uma foice caindo/` — variações candidatas para o som de foice caindo

---

## 4. Trilhas Musicais (`/assets/audio/music/`)

As trilhas musicais acompanham os três estados principais da jornada do jogador.

| Arquivo | Aplicação | Evento de Acionamento |
|---|---|---|
| `musicaVilarejoMUSIC.mp3` | Trilha do Vilarejo | Entrada/exploração na área do vilarejo |
| `musicaCastelloMUSIC.mp3` | Trilha do Castelo | Entrada/exploração na área do castelo |
| `musicaBossMUSIC.mp3` | Trilha do Chefe | Início do encontro com o líder do culto |

### Diretrizes de uso

- As trilhas de área (**Vilarejo** e **Castelo**) devem ser reproduzidas em **loop** durante a exploração e os combates comuns das respectivas zonas.
- A trilha de boss substitui a trilha de área no momento do **encontro com o chefe**, devendo haver transição clara entre elas.
- Evitar sobreposição entre trilhas: ao ativar `musicaBossMUSIC`, a trilha de área corrente deve ser interrompida ou silenciada.

---

## 5. Sons de Ambiente (`/assets/audio/amb/`)

Sons atmosféricos contínuos que reforçam a ambiência de cada área sem sobreposição com o sistema de trilhas musicais.

| Arquivo | Aplicação | Área Associada |
|---|---|---|
| `ventoLeveAMB.mp3` | Vento suave | Vilarejo |
| `ventoForteAMB.mp3` | Vento forte | Castelo |

### Diretrizes de uso

- Reproduzir em **loop** com volume baixo, subordinado à trilha musical de área.
- Realizar crossfade entre `ventoLeveAMB` e `ventoForteAMB` na transição entre Vilarejo e Castelo para reforçar a mudança de ambiente.


---

## 6. Sons de Interface (`/assets/audio/ui/`)

| Arquivo | Aplicação | Evento de Acionamento |
|---|---|---|
| `musicaTelainicialUI.mp3` | Música da Tela Inicial | Abertura do jogo / exibição do menu principal |
| `optionsButtonUI.mp3` | Clique de botão | Interação com qualquer botão de menu ou opções |

### Diretrizes de uso

- `musicaTelainicialUI.mp3` deve tocar em **loop** enquanto o jogador estiver na tela inicial e ser interrompida ao iniciar uma partida.
- `optionsButtonUI.mp3` deve ser acionado **ao pressionar** qualquer botão de UI, mantendo volume levemente abaixo da música de fundo para não sobrepor.

---

## 7. Efeitos Sonoros de Gameplay (`/assets/audio/sfx/`)

### 7.1 Ações do Jogador — A Morte

| Arquivo | Ação | Evento de Acionamento |
|---|---|---|
| `puloSFX.mp3` | Pulo | Ao pressionar o botão de pulo |
| `efeitoDashSFX.mp3` | Dash | Ao executar o movimento de dash |
| `ataqueFoice3SFX.mp3` | Ataque com Foice | Ao executar o ataque padrão com a foice |
| `combateFoice1SFX.mp3` | Impacto da Foice (variação 1) | Ao acertar um inimigo com a foice |
| `combateFoice2SFX.mp3` | Impacto da Foice (variação 2) | Variação aleatória ao acertar um inimigo |

> **Nota sobre variações de ataque:** Os arquivos `combateFoice1SFX` e `combateFoice2SFX` devem ser acionados alternadamente ou de forma aleatória a cada acerto bem-sucedido, evitando repetição monotônica.

---

### 7.2 Morte do Jogador

A morte do jogador é representada por uma sequência sonora composta de dois eventos distintos e simultâneos:

| Arquivo | Evento | Descrição |
|---|---|---|
| `evaporandoSFX.mp3` | Corpo da Morte se dissolve | O personagem se desfaz em pó/partículas ao morrer |
| `foiceCaindo1SFX.mp3` | Foice cai (variação 1) | Após a dissolução, a foice é abandonada e cai ao chão |
| `foiceCaindo2SFX.mp3` | Foice cai (variação 2) | — |
| `foiceCaindo3SFX.mp3` | Foice cai (variação 3) | — |
| `foiceCaindo4SFX.mp3` | Foice cai (variação 4) | — |
| `foiceCaindo5SFX.mp3` | Foice cai (variação 5) | — |

**Sequência de execução sugerida:**

1. `evaporandoSFX.mp3` — acionado no instante da morte
2. Uma variação aleatória de `foiceCaindo[1–5]SFX.mp3` — acionada em seguida, com leve delay correspondente à animação

---

### 7.3 Inimigos — Ocultistas

O jogo possui dois tipos de inimigo: **Melee** (corpo a corpo) e **Ranged** (à distância, com bolas de fogo).

| Arquivo | Inimigo | Ação | Evento de Acionamento |
|---|---|---|---|
| `ataqueMelee1ANDRangerSFX.mp3` | Melee e Ranged | Ataque (variação 1) | Ao iniciar o ataque do inimigo Melee ou Ranged |
| `ataqueMelee2SFX.mp3` | Melee | Ataque (variação 2) | Variação do ataque corpo a corpo |
| `explosaoAtaqueRangerSFX.mp3` | Ranged | Explosão da bola de fogo | Ao impactar a bola de fogo do ocultista Ranged |

**Impactos com sangue** (subpasta `BloodHit/`):

| Arquivo | Aplicação |
|---|---|
| `bloodHit1SFX.mp3` | Som de impacto com sangue ao acertar inimigo (variação 1) |
| `bloodHit2SFX.mp3` | Som de impacto com sangue ao acertar inimigo (variação 2) |
| `bloodHit3SFX.mp3` | Som de impacto com sangue ao acertar inimigo (variação 3) |

> Os arquivos `BloodHit` devem ser reproduzidos aleatoriamente a cada acerto no inimigo, complementando os sons da foice.

---

### 7.4 Chefe — Líder do Culto

O chefe possui dois padrões de ataque distintos, cada um com seu próprio conjunto de sons.

**Ataque 1 — Lançamento de Bolas de Fogo (no chão):**

| Arquivo | Evento |
|---|---|
| `lancamentoBolaDeFogoBossSFX.mp3` | Disparo da bola de fogo pelo chefe |
| `explosaoBolaDeFogoBossSFX.mp3` | Explosão ao impactar a bola de fogo |

**Ataque 2 — Ataque Aéreo (o chefe vai ao céu):**

| Arquivo | Evento |
|---|---|
| `explosaoAtaqueBossCeuSFX.mp3` | Explosão dos projéteis lançados do céu pelo chefe |

---

### 7.5 Portas e Transições de Área

| Arquivo | Aplicação | Localização |
|---|---|---|
| `portaAbrindoSFX.mp3` | Porta genérica abrindo | Geral |
| `portaFechandoSFX.mp3` | Porta genérica fechando | Geral |
| `portaAbrindoEntradaCasteloSFX.mp3` | Porta de entrada do castelo abrindo | Transição Vilarejo → Castelo |
| `portaFechandoEntradaCasteloSFX.mp3` | Porta de entrada do castelo fechando | Transição Vilarejo → Castelo |
| `portaAbrindoSaidaCasteloSFX.mp3` | Porta de saída do castelo abrindo | Interior do Castelo |
| `portaFechandoSaindaCasteloSFX.mp3` | Porta de saída do castelo fechando | Interior do Castelo |

---

## 8. Inventário Completo de Áudio

### 8.1 Resumo por categoria

| Categoria | Quantidade de Arquivos |
|---|---|
| Trilhas musicais (`music/`) | 3 |
| Sons de ambiente (`amb/`) | 2 |
| Sons de interface (`ui/`) | 2 |
| SFX — Jogador | 5 |
| SFX — Morte do jogador | 6 |
| SFX — Inimigos | 3 |
| SFX — Impacto com sangue (`BloodHit/`) | 3 |
| SFX — Chefe | 3 |
| SFX — Portas / Ambiente | 6 |
| **Total** | **33** |

---

## 9. Mapa de Eventos Sonoros

| Momento do Jogo | Áudio Acionado |
|---|---|
| Abertura / Menu principal | `musicaTelainicialUI.mp3` (loop) |
| Clique em botão de menu | `optionsButtonUI.mp3` |
| Entrada no Vilarejo | `musicaVilarejoMUSIC.mp3` + `ventoLeveAMB.mp3` |
| Exploração do Vilarejo | `musicaVilarejoMUSIC.mp3` + `ventoLeveAMB.mp3` (loops) |
| Combate no Vilarejo | `musicaVilarejoMUSIC.mp3` (mantida) |
| Porta de entrada do Castelo | `portaAbrindoEntradaCasteloSFX.mp3` / `portaFechandoEntradaCasteloSFX.mp3` |
| Entrada no Castelo | `musicaCastelloMUSIC.mp3` + `vendoForteAMB.mp3` |
| Porta interna do Castelo | `portaAbrindoSFX.mp3` / `portaFechandoSFX.mp3` |
| Saída interna do Castelo | `portaAbrindoSaidaCasteloSFX.mp3` / `portaFechandoSaindaCasteloSFX.mp3` |
| Pulo do jogador | `puloSFX.mp3` |
| Dash do jogador | `efeitoDashSFX.mp3` |
| Ataque com foice | `ataqueFoice3SFX.mp3` |
| Acerto na foice em inimigo | `combateFoice[1–2]SFX.mp3` (aleatório) + `bloodHit[1–3]SFX.mp3` (aleatório) |
| Ataque de inimigo Melee/Ranged | `ataqueMelee1ANDRangerSFX.mp3` ou `ataqueMelee2SFX.mp3` (aleatório) |
| Explosão de bola de fogo do Ranged | `explosaoAtaqueRangerSFX.mp3` |
| Encontro com o Chefe | `musicaBossMUSIC.mp3` (substitui trilha de área) |
| Chefe lança bola de fogo | `lancamentoBolaDeFogoBossSFX.mp3` |
| Explosão de bola de fogo do Chefe | `explosaoBolaDeFogoBossSFX.mp3` |
| Chefe ataca do céu | `explosaoAtaqueBossCeuSFX.mp3` |
| Morte do jogador | `evaporandoSFX.mp3` + `foiceCaindo[1–5]SFX.mp3` (aleatório, com delay) |

---

## 10. Convenções de Nomenclatura

O projeto adota o seguinte padrão de nomenclatura para todos os arquivos de áudio:

```
[descrição][Categoria][TIPO].mp3
```

| Sufixo | Significado | Pasta |
|---|---|---|
| `MUSIC` | Trilha musical de área | `music/` |
| `AMB` | Som de ambiente/atmosfera | `amb/` |
| `UI` | Áudio de interface | `ui/` |
| `SFX` | Efeito sonoro de gameplay | `sfx/` |

**Arquivos com inconsistência identificada:**

| Arquivo atual | Correção sugerida | Problema |
|---|---|---|
| `vendoForteAMB.mp3` | `ventoForteAMB.mp3` | Erro tipográfico (`vendo` → `vento`) |
| `lancamentoBolaDeFogoBossSPX.mp3` | `lancamentoBolaDeFogoBossSFX.mp3` | Erro tipográfico (`SPX` → `SFX`) |

---

## 11. Parâmetros de Mixagem

- **SFX de combate** devem ter prioridade de volume sobre as trilhas musicais para garantir clareza de feedback
- **Sons de ambiente** (`amb/`) devem operar em volume reduzido (sugerido: 20–35% do volume máximo), subordinados às trilhas
- **Sons de morte do jogador** (`evaporandoSFX` + `foiceCaindo`) devem ter destaque claro — sugerido fade ou pausa momentânea da trilha musical
- **Trilha do Chefe** deve ter volume ligeiramente superior à trilha de área para reforço dramático
- **Sons de porta** não devem ser cortados abruptamente — permitir reprodução completa mesmo em transições rápidas
- **Variações de `bloodHit` e `combateFoice`** devem ser randomizadas para evitar repetição perceptível em combates prolongados

---

## 12. Padronização Estética

- Todos os sons devem ser coerentes com a identidade **sombria, etérea e sobrenatural** da protagonista
- SFX do jogador devem ter textura **mística/espectral**, diferenciando A Morte de personagens comuns
- Ataques de inimigos devem soar **viscerais e concretos**, em contraste com a leveza sobrenatural da protagonista
- Trilhas musicais devem crescer em **tensão e dramaticidade** ao longo da progressão: Vilarejo → Castelo → Boss

---

## 13. Restrições Atuais

- Sistema de mixagem ainda simplificado, sem controle dinâmico de volume por camadas
- Ausência de efeitos de transição entre trilhas (sem crossfade implementado)
- Sem áudio espacial/posicional implementado
- Sem sons de interface para vitória, derrota ou game over
- Sem variações de SFX para o ataque da foice (apenas `ataqueFoice3SFX` disponível; variações 1 e 2 ausentes)
- Sem sons de dano recebido pelo jogador
- Sons de ambiente e música ainda não implementados como sistema de camadas

---

## 14. Melhorias Futuras

- Implementar **crossfade dinâmico** entre trilhas de exploração e combate
- Adicionar **variações de ataque da foice** (`ataqueFoice1SFX`, `ataqueFoice2SFX`) para complementar a variação 3 existente
- Criar sons de **dano recebido pelo jogador**
- Adicionar sons de **vitória, game over e tela de créditos**
- Implementar **controle de volume** separado para música, SFX e ambiente na tela de opções
- Desenvolver **sistema de camadas de áudio** (trilha + ambiente simultâneos com controle independente)
- Adicionar sons para **queda do jogador** ao pousar após pulo
- Implementar **áudio espacial/posicional** para inimigos e eventos de área
- Adicionar sons de **UI adicionais** (hover de botão, abertura/fechamento de menus)
- Criar sons exclusivos para a **fase de morte do chefe**

---

## 15. Considerações Finais

Os arquivos de áudio atualmente presentes no projeto foram obtidos a partir de fontes identificadas como livres de direitos autorais, destinados ao uso em fase de prototipagem e desenvolvimento. Antes de qualquer publicação comercial ou lançamento público do projeto, recomenda-se:

1. Validar a licença de cada arquivo individualmente
2. Substituir por assets licenciados comercialmente ou produzidos exclusivamente para o projeto
3. Documentar os créditos de cada arquivo de áudio utilizado

O sistema de áudio atual cobre os principais estados de gameplay — menu, exploração, combate e chefe — estabelecendo uma base funcional e coerente com a identidade dark fantasy do jogo para as fases seguintes de desenvolvimento.

---

*Documento gerado com base na estrutura real de arquivos do projeto em junho de 2026.*
*Versão 2.0 — Substituição completa do SDD v1.0.*
