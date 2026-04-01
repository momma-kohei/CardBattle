using System;
using UnityEngine;

// 修正版
public class Card_mdl
{
    // カードID
    int _id;

    // IDから一意に定まる情報
    string _name;
    EleType _type;
    int _power;

    // ScriptableObjectから取得する情報
    Sprite _texture;
    //string _description; // 未使用

    public Card_mdl(int id) // コンストラクタ
    {
        if (!IsVarid(id))
        {
            throw new ArgumentOutOfRangeException("id"); // IDが範囲外の場合は例外を投げる
        }
        
        _id = id; // IDが範囲内の場合のみ数値をIDに格納

        _name = GuessName(id);
        _type = GuessType(id);
        _power = GuessPower(id);

        _texture = GetTexture(id);
        //_description = GetDescription(id);
    }

    // UI表示用ゲッター
    #region Getter
    public int GetID()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public EleType GetEleType()
    {
        return _type;
    }

    public int GetPower()
    {
        return _power;
    }

    public Sprite GetTexture()
    {
        return _texture;
    }
    #endregion

    // ----------内部メソッド----------

    bool IsVarid(int id) // 例外処理
    {
        return (id >= 10 && id <= 39 );
    }

    string GuessName(int id) // IDからカード名前を類推
    {
        switch (id % 10)
        {
            case 0: return "子猫";
            case 1: return "一般猫";
            case 2: return "猫兵士";
            case 3: return "猫戦士";
            case 4: return "猫魔導士";
            case 5: return "猫騎士";
            case 6: return "猫将軍";
            case 7: return "猫勇者";
            case 8: return "猫王";
            case 9: return "ライオン";
        }
        return "違法猫";
    }

    EleType GuessType(int id) // IDから属性を類推
    {
        return (EleType) (id / 10); // 十の位
    }

    int GuessPower(int id) // IDからパワーを類推
    {
        return (int) (id % 10) + 1; // 一の位
    }

    Sprite GetTexture(int id) // カードデータと照合して画像を取得
    {
        CardData cardData = Resources.Load<CardData>("CardDatas/Card" + id); // 場所とファイル名でロードするデータを指定
        return cardData.cardTexture;
    }

    string GetDescription(int id) // カードデータと照合して説明文を取得
    {
        CardData cardData = Resources.Load<CardData>("CardDatas/Card" + id); // 場所とファイル名でロードするデータを指定
        return cardData.description;
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
    Grass // 草属性；ポケモンに倣って草をGrassとした
}
