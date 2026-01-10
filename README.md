# CardBattle
カードバトル製作

## 環境構築
### TextMesh Pro 
TextMesh Pro（TMP）はデフォルトで日本語を表示できず，日本語のデータファイルは非常に重くGitHubに共有できないため各自で環境構築を行う．
<br>日本語フォントファイルは「/Assets/TextMesh Pro/Fonts/」に保存する．
<br>※ここに保存することで，TMPに関するファイルおよびフォルダをgitignoreに追加したためコミット時にTMPに関するデータは無視され，プッシュもされない．

参考：https://zenn.dev/kametani256/articles/63c083ab318136

## ファイル構成
MVP設計を目標として開発している．
<pre>
.
└── Assets
    ├── Prefabs
    │   └── CardPrefab // 共通のカードプレハブ
    ├── Resources
    │    └── CardDatas // カードデータのスクリプタブルオブジェクトをまとめたファイル
    │        ├── Card10
    │        │ ...
    │        └── Card39
    ├── Scenes
    │   └── BattleScene
    ├── Scripts
    │   ├── CardSystem
    │   │    └── CardData // スクリプタブルオブジェクトの設定スクリプト
    │   ├── LayoutGroup
    │   │    └── HandLayoutGroup // 手札のオブジェクトレイアウトに関する制御を行うスクリプト
    │   ├── Models
    │   │    ├── CardModel
    │   │    ├── ListClasses
    │   │    ├── PlayerModel
    │   │    └── TableModel
    │   ├── Presenters
    │   │    └── TablePresenter
    │   ├── Views
    │   │    ├── CardListsView // 各カード表示位置の制御を行うスクリプト群
    │   │    │    ├── Area1View
    │   │    │    ├── Area2View
    │   │    │    ├── Hand1View
    │   │    │    └── Hand2View
    │   │    ├── ButtonView
    │   │    ├── CardView
    │   │    └── PlayerView
    │   └── GameManager
    ├── Textures // 使用するテクスチャを保存するフォルダ
    │   
    │   手動で変更しないファイル
    ├── Settings
    ├── Default Volume Profile
    ├── Input System_Actions
    ├── Universal Render Pipeline Global Settings
    └── TextMesh Pro
</pre>
