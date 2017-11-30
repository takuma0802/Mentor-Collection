using UnityEngine;

[System.SerializableAttribute]
public class MstCharacter
{
	[SerializeField]
	private int
	_id,
	_rarity,
	_maxLebel,
	_growthType,
	_lowerEnergy,
	_upperEnergy,
	_initialCost;

	[SerializeField]
	private string
	_name,
	_imageId,
	_featureText;

	public void SetFromCsv(string[] data)
	{
		_id          = int.Parse (data [0]);
		_name        = data[1];
		_imageId     = data[2];
		_featureText = data[3];
		_rarity      = int.Parse( data[4] );
		_maxLebel    = int.Parse( data[5] );
		_growthType  = int.Parse( data[6] );
		_lowerEnergy = int.Parse( data[7] );
		_upperEnergy = int.Parse( data[8] );
		_initialCost = int.Parse( data[9] );
	}

	public int ID { get { return _id; }}
	public int Rarity { get { return _rarity; }}
	public int MaxLebel { get { return _maxLebel; }}
	public int GrowthType { get { return _growthType; }}
	public int LowerEnergy { get { return _lowerEnergy; }}
	public int UpperEnergy { get { return _upperEnergy; }}
	public int InitialCost { get { return _initialCost; }}
	public string Name { get { return _name; }}
	public string ImageId { get { return _imageId; }}
	public string FeatureText { get { return _featureText; }}

	public bool PurchaseAvailable(int currentMoney)
	{
		return (currentMoney >= _initialCost);
	}
}