using System;
using UnityEngine;

public class Card_mdl
{
    // カードID
    int     _id;
    string  _name;
    EleType _type;
    int     _power;
    Sprite _texture;
    string _description; // 未使用

    // プロパティ
    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public EleType Type { get { return _type; } }
    public int Power { get { return _power; } }
    public Sprite Texture { get { return _texture; } }
    public string Description { get { return _description; } }

    public Card_mdl(int id) // コンストラクタ
    {
        if (!IsVarid(id)) throw new ArgumentOutOfRangeException("id"); // IDが範囲外の場合は例外を投げる
        _id = id; // IDが範囲内の場合のみ数値をIDに格納
        GetCardInfo(id); // カードデータと照合してカードデータを取得
    }

    // ----------内部メソッド----------

    bool IsVarid(int id) // 例外処理
    {
        return (id >= 1 && id <= 30 );
    }

    void GetCardInfo(int id) // カードデータと照合してカードデータを取得
    {
        CardData data = Resources.Load<CardData>("CardDatas/" + id); // 場所とファイル名でロードするデータを指定
        _name = data.cardName;
        _type = data.cardType;
        _power = data.cardPower;
        _texture = data.cardTexture;
        _description = data.description;
    }
}

/// <summary>
/// 属性の種類を定義する列挙型
/// </summary>
public enum EleType
{
    None, // 未設定
    Fire, // 火属性
    Water, // 水属性
    Grass // 草属性
}
