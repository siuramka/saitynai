import {
  Card,
  CardContent,
  Typography,
  Grid,
  CardActionArea,
} from "@mui/material";
import { IShop } from "../../interfaces/Shop/IShop";
import { useContext, useEffect, useState } from "react";
import { getShops } from "../../services/shop";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../utils/context/AuthContext";
import CreateShopModal from "./CreateShopModal";
import EditShopModal from "./EditShopModal";

const ShopsList = () => {
  const navigate = useNavigate();
  const { user } = useContext(AuthContext);

  const [shops, setShops] = useState<IShop[]>();
  const [refreshState, setRefreshState] = useState(false);

  const handleRefresh = () => {
    setRefreshState((prevRefreshState) => !prevRefreshState);
  };

  useEffect(() => {
    getShops()
      .then((shops) => {
        setShops(shops?.data);
      })
      .catch(() => {});
  }, [refreshState]);

  return (
    <div>
      <h1>Shops</h1>

      {user && user.role === "ShopSeller" && (
        <>
          <CreateShopModal handleRefresh={handleRefresh} />
        </>
      )}
      <Grid container spacing={3}>
        {shops &&
          shops.length > 0 &&
          shops.map((shop) => {
            return (
              <Grid item xs={12} sm={6} md={4} lg={4} key={shop.id}>
                <CardActionArea
                  onClick={() => navigate(`/dashboard/shops/${shop.id}`)}
                >
                  <Card sx={{ minWidth: 300, minHeight: 200, maxHeight: 200 }}>
                    <CardContent>
                      <Typography
                        sx={{ fontSize: 14 }}
                        color="text.secondary"
                        gutterBottom
                      >
                        {shop.name}
                      </Typography>
                      <Typography variant="h5" component="div"></Typography>
                      <Typography sx={{ mb: 1.5 }} color="text.secondary">
                        {shop.description}
                      </Typography>
                      <Typography variant="body2">
                        {shop.contactInformation}
                      </Typography>
                    </CardContent>
                  </Card>
                </CardActionArea>
                {/** TODO: single modal for multiple edits */}
                {user && user.role === "ShopSeller" && (
                  <>
                    <EditShopModal handleRefresh={handleRefresh} shop={shop} />
                  </>
                )}
              </Grid>
            );
          })}
      </Grid>
    </div>
  );
};

export default ShopsList;
