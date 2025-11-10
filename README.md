# CardBattle
カードバトル製作

## 環境構築
### TextMesh Pro 
TextMesh Pro（TMP）はデフォルトで日本語を表示できず，日本語のデータファイルは非常に重くGit Hubに共有できないため各自で環境構築を行う．
<br>※TMPに関するファイルおよびフォルダをgitignoreに追加したためコミット時にTMPに関するデータは無視され，プッシュもされない．

参考：https://zenn.dev/kametani256/articles/63c083ab318136

## ファイル構造
<pre>
.
└── Assets
    ├── Prefabs
    │   ├── FieldCardPrefab // クリックできないフィールドカードのプレハブ
    │   └── HandCardPrefab // クリックできる手札カードのプレハブ
    ├── Resources
    │    └── CardDatas // カードデータのスクリプタブルオブジェクトをまとめたファイル
    │        ├── Card11
    │        │ ...
    │        └── Card38
    ├── Scenes
    │   └── BattleScene
    ├── Scripts
    │   ├── CardSystem
    │   │    └── CardData // スクリプタブルオブジェクトの設定スクリプト
    │   ├── CardController
    │   ├── CardManager // 仮実装
    │   ├── CardModel
    │   ├── CardView
    │   └── GameManager
    ├── Textures
    │   
    │   手動で変更しないファイル
    ├── Settings
    ├── Default Volume Profile
    ├── Input System_Actions
    ├── Universal Render Pipeline Global Settings
    └── TextMesh Pro
</pre>
