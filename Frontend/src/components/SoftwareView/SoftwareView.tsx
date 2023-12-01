import { Button, Chip, Grid, Paper, TextField } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getShopSoftware } from "../../services/software";
import { ISoftware } from "../../interfaces/Software/ISoftware";
import { useDispatch } from "react-redux";
import { addToCart } from "../../features/CartSlice";
import { ICartItem } from "../../interfaces/ICartItem";
import { AuthContext } from "../../utils/context/AuthContext";

const SoftwareView = () => {
  const { shopId, softwareId } = useParams();
  const softwareIdNum = Number(softwareId);
  const shopIdNum = Number(shopId);

  const { user } = useContext(AuthContext);

  const [subscriptionTerm, setSubscriptionTerm] = useState<number>(1);

  const dispatch = useDispatch();

  const [software, setSoftware] = useState<ISoftware>();

  const handleAddToCart = (software: ISoftware) => {
    const cartItem: ICartItem = {
      software: software,
      subscription: { TermInMonths: subscriptionTerm },
      shopId: shopIdNum,
    };
    dispatch(addToCart(cartItem));
  };

  const handleSubscriptionTermChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const newVal = Number(event.target.value);

    newVal > 24
      ? setSubscriptionTerm(24)
      : setSubscriptionTerm(Number(event.target.value));
  };

  useEffect(() => {
    getShopSoftware({ shopId: shopIdNum, softwareId: softwareIdNum })
      .then((softwareData) => setSoftware(softwareData))
      .catch();
  }, [softwareId]);

  return (
    software && (
      <Grid container spacing={2}>
        <Grid item md={8}>
          <Paper sx={{ padding: "50px" }}>
            <Grid container spacing={2}>
              <Grid item md={12}>
                <h1 style={{ textTransform: "uppercase" }}>{software.name}</h1>
              </Grid>
              <Grid item md={12}>
                <h3>Description:</h3>
                {software.description}
              </Grid>
              <Grid item md={12}>
                <h3>Installation instructions:</h3>
                {software.instructions}
              </Grid>
            </Grid>
          </Paper>
        </Grid>
        <Grid item md={4}>
          <Paper sx={{ padding: "20px" }}>
            <Grid container spacing={2}>
              <Grid item md={12}>
                <h4>Program/Website </h4>
                <h4>
                  <Chip label={software.website} />
                </h4>
              </Grid>
            </Grid>
          </Paper>
          <Paper sx={{ padding: "50px", marginTop: 2 }}>
            <Grid container spacing={2}>
              <Grid
                item
                md={12}
                sx={{
                  display: "flex",
                  justifyContent: "center",
                }}
              >
                {software.priceMonthly === 0 ? (
                  <h2>
                    Price: <Chip label="FREE" />
                  </h2>
                ) : (
                  <h2>Price: {software.priceMonthly}$ per month</h2>
                )}
              </Grid>
              {user && user.role === "ShopUser" && (
                <Grid
                  item
                  md={12}
                  sx={{
                    display: "flex",
                    justifyContent: "center",
                  }}
                >
                  <TextField
                    type="number"
                    label="Months"
                    value={subscriptionTerm}
                    inputProps={{ min: 1, max: 24 }}
                    onChange={handleSubscriptionTermChange}
                  />
                  <Button
                    variant="outlined"
                    color="success"
                    onClick={() => handleAddToCart(software)}
                  >
                    Add to cart
                  </Button>
                </Grid>
              )}
            </Grid>
          </Paper>
        </Grid>
      </Grid>
    )
  );
};

export default SoftwareView;
