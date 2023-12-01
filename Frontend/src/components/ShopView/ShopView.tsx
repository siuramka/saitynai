import { useEffect, useState } from "react";
import SoftwaresList from "./SoftwaresList";
import { IShop } from "../../interfaces/Shop/IShop";
import { useParams } from "react-router-dom";
import { getShop } from "../../services/shop";

const ShopView = () => {
  const [shop, setShop] = useState<IShop>();
  const { id } = useParams();
  const shopId = Number(id);

  useEffect(() => {
    getShop({ shopId: shopId }).then((shopData) => setShop(shopData));
  }, []);

  return (
    <>
      {shop && <h2>{shop.name}</h2>}
      <SoftwaresList />
    </>
  );
};

export default ShopView;
