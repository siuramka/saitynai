import { useSelector, useDispatch } from "react-redux";
import {
  removeFromCart,
  selectItems,
  clearCart,
} from "../../features/CartSlice";
import { Box, Button, Chip, IconButton, Paper, Stack } from "@mui/material";
import React from "react";
import { ICartItem } from "../../interfaces/ICartItem";
import RemoveCircleIcon from "@mui/icons-material/RemoveCircle";
import { createSubscription } from "../../services/subscription";

const Cart = React.forwardRef((props, ref) => {
  const items = useSelector(selectItems);
  const dispatch = useDispatch();

  const handleOrder = () => {
    const promises = items.map((item) => {
      return createSubscription({
        subscription: item.subscription,
        shopId: item.shopId,
        softwareId: item.software.id,
      });
    });

    Promise.all(promises).then(() => {
      dispatch(clearCart());
      alert("Order successful!");
    });
  };

  const handleRemoveFromCart = (item: ICartItem) => {
    dispatch(removeFromCart(item));
  };

  return (
    <Paper sx={{ p: "20px" }}>
      <Stack spacing={2}>
        {items.map((item: ICartItem) => (
          <Box
            key={`${item.software.id}+${item.subscription.TermInMonths}`}
            sx={{
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
            }}
          >
            <Box>
              {item.software.name}{" "}
              <Chip
                label={`${item.subscription.TermInMonths} months`}
                color="primary"
                variant="outlined"
              />
            </Box>
            <Box>
              <IconButton
                onClick={() => handleRemoveFromCart(item)}
                sx={{ p: 0 }}
              >
                <RemoveCircleIcon />
              </IconButton>
            </Box>
          </Box>
        ))}
      </Stack>
      <Button variant="contained" onClick={handleOrder}>
        Order
      </Button>
    </Paper>
  );
});

export default Cart;
