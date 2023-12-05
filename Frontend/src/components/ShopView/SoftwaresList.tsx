import { useContext, useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { ISoftware } from "../../interfaces/Software/ISoftware";
import {
  deleteSoftware,
  getAllSoftwares,
  getShopSoftwares,
} from "../../services/software";
import {
  Grid,
  Card,
  CardActionArea,
  CardContent,
  Typography,
  Button,
} from "@mui/material";
import CreateSoftwareModal from "./CreateSoftwareModal";
import EditSoftwareModal from "./EditSoftwareModal";
import { useSelector } from "react-redux";
import { selectUser } from "../../features/AuthSlice";

const SoftwaresList = () => {
  const navigate = useNavigate();
  const user = useSelector(selectUser);

  const { id } = useParams();
  const shopId = Number(id);

  const [softwares, setSoftwares] = useState<ISoftware[]>();
  const [refreshState, setRefreshState] = useState(false);

  const handleRefresh = () => {
    setRefreshState((prevRefreshState) => !prevRefreshState);
  };

  const handleDelete = (softwareId: number, shopId: number) => {
    deleteSoftware({ shopId: shopId, softwareId: softwareId }).then(() => {
      handleRefresh();
    });
  };

  useEffect(() => {
    if (user && user.role === "Admin") {
      getAllSoftwares().then((softwaresData) => setSoftwares(softwaresData));
    } else {
      getShopSoftwares({ shopId: shopId }).then((softwaresData) =>
        setSoftwares(softwaresData)
      );
    }
  }, [shopId, refreshState]);

  return (
    <>
      {user && user.role === "ShopSeller" && (
        <>
          <CreateSoftwareModal shopId={shopId} handleRefresh={handleRefresh} />
        </>
      )}
      {softwares && softwares.length > 0 ? (
        <Grid container spacing={3}>
          {softwares.map((soft) => {
            return (
              <Grid item xs={12} sm={6} md={4} lg={4} key={soft.id}>
                <CardActionArea
                  onClick={() => {
                    navigate(`/dashboard/shops/${shopId}/softwares/${soft.id}`);
                  }}
                >
                  <Card sx={{ minWidth: 300, minHeight: 200, maxHeight: 200 }}>
                    <CardContent>
                      <Typography
                        sx={{ fontSize: 14 }}
                        color="text.secondary"
                        gutterBottom
                      >
                        {soft.name}
                      </Typography>
                      <Typography variant="h5" component="div"></Typography>
                      <Typography sx={{ mb: 1.5 }} color="text.secondary">
                        {soft.description}
                      </Typography>
                      <Typography variant="body2">
                        {soft.priceMonthly === 0 ? (
                          <>Free</>
                        ) : (
                          soft.priceMonthly
                        )}
                      </Typography>
                    </CardContent>
                  </Card>
                </CardActionArea>
                {user && user.role === "ShopSeller" && (
                  <>
                    <EditSoftwareModal
                      handleRefresh={handleRefresh}
                      software={soft}
                      shopId={shopId}
                    />
                  </>
                )}
                {user && user.role === "Admin" && (
                  <Button onClick={() => handleDelete(soft.id, shopId)}>
                    Delete
                  </Button>
                )}
              </Grid>
            );
          })}
        </Grid>
      ) : (
        <>This shop has no available softwares.</>
      )}
    </>
  );
};

export default SoftwaresList;
