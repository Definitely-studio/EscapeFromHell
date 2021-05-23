using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public List<InventorySlot> CollectedItemSlots;
	public List<InventorySlot> EquippedItemSlots;
	public List<InventorySlot> StoryItemSlots;
	
	public Transform CollectedItem;
	public Transform EquippedItem;
	public Transform StoryItem;
    public List<Item> ItemList = new List<Item>();
	public GameObject inventoryUI;
	[SerializeField] private Input _input;
	[SerializeField] private GameObject crosshair;
	private bool isInventoryActive = true;
	private GameObject OverlapedItem;


  private void Awake()
    {

        _input = new Input();
        _input.Player.Invent.performed += context => ActivateInventory();
		_input.Player.Submit.performed += context => Pickup();

    }

	private void Start() 
	{
		CollectSlots();
	}

	public InventoryManager() 
	{
		
	}
	  private void OnEnable()
    {
        _input.Enable();
    }


	private void CollectSlots()
	{
		for (int i = 0; i< EquippedItem.childCount; i++)
		{
			if(EquippedItem.GetChild(i).TryGetComponent<InventorySlot>(out InventorySlot slot))
				EquippedItemSlots.Add(slot);
		}

		for (int i = 0; i< CollectedItem.childCount; i++)
		{
			if(CollectedItem.GetChild(i).TryGetComponent<InventorySlot>(out InventorySlot slot))
				CollectedItemSlots.Add(slot);
		}

		for (int i = 0; i< StoryItem.childCount; i++)
		{
			if(StoryItem.GetChild(i).TryGetComponent<InventorySlot>(out InventorySlot slot))
				StoryItemSlots.Add(slot);
		}
	}



	private void ActivateInventory()
	{
	
		if (isInventoryActive)  {
				inventoryUI.SetActive(false);
				isInventoryActive = false;
				Cursor.visible = false;
        		crosshair.SetActive(true);
    
				Time.timeScale = 1.0f;
			}
				else{
				inventoryUI.SetActive(true);
				isInventoryActive = true;
				Cursor.visible = true;
        		crosshair.SetActive(false);
				Time.timeScale = 0.0f;
			}	
	}

	private void Pickup()
	{
		if(OverlapedItem != null)
		{
			AddItem(OverlapedItem);
			OverlapedItem.SetActive(false);
		}
	}

	public void AddItem(GameObject itemObject) {

		Item item = itemObject.GetComponent<Item>();
		if(item.Type == Item.ItemType.RangedWeaponItem)
		{
			foreach (InventorySlot slot in EquippedItemSlots)
			{
				if(slot.Item == null){
					
					slot.Item = item;
					slot.ItemObject = itemObject;
					slot.AmountText.enabled = true;
					slot.Amount = item.Amount;	
					slot.AmountText.text = slot.Amount.ToString();
					slot.IconImage.sprite = item.Icon;	
					slot.IconImage.color = new Color(1,1,1,1);
					return;
				}
			}
		}

		foreach (InventorySlot slot in CollectedItemSlots)
		{
			if(slot.Item != null){
				if(slot.Item.Type == item.Type)
				{
					slot.Amount += item.Amount;
					slot.AmountText.text = slot.Amount.ToString();
					return;		
				}
			}
		}

		foreach (InventorySlot slot in CollectedItemSlots)
		{
			if(slot.Item == null){

				slot.Item = item;
				slot.ItemObject = itemObject;
				slot.AmountText.enabled = true;
				slot.Amount = item.Amount;	
				slot.AmountText.text = slot.Amount.ToString();
				slot.IconImage.sprite = item.Icon;	
				slot.IconImage.color = new Color(1,1,1,1);
				
				return;
			}
		}
		return;
	}

	public void OverlapItem(GameObject OverlapedItem)
	{
		Debug.Log("OverlapItem");
		this.OverlapedItem = OverlapedItem;
	}

	public void OverlapItem()
	{
		this.OverlapedItem = null;
	}

}