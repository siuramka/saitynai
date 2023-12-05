import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import { useNavigate } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import {
  styled,
  Badge,
  BadgeProps,
  Paper,
  Popover,
  Fade,
  Popper,
} from "@mui/material";
import { useDispatch, useSelector } from "react-redux";

import Cart from "../../../CartView/CartView";
import CartView from "../../../CartView/CartView";
import { selectItemsCountCount } from "../../../../features/CartSlice";
import { removeUser, selectUser } from "../../../../features/AuthSlice";

const UserHeader = () => {
  const navigate = useNavigate();
  const user = useSelector(selectUser);
  const dispatch = useDispatch();
  
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(
    null
  );

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const cartCount = useSelector(selectItemsCountCount);
  const [anchorElCart, setAnchorElCart] = useState<HTMLElement | null>(null);

  const [openCart, setOpenCart] = useState(false);

  const handleCartClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElCart(event.currentTarget);
    setOpenCart((prev) => !prev);
  };

  useEffect(() => {}, [cartCount]);

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Typography
            variant="h6"
            noWrap
            component="a"
            href="#app-bar-with-responsive-menu"
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            E-BOTS
          </Typography>

          <Box
            sx={{
              flexGrow: 1,
              display: { md: "flex", sm: "flex", xs: "flex" },
            }}
          >
            <Button
              onClick={() => navigate("/dashboard/shops")}
              sx={{ my: 2, color: "white", display: "block" }}
            >
              Shops
            </Button>
            <Button
              onClick={() => navigate("/dashboard/subscriptions")}
              sx={{ my: 2, color: "white", display: "block", ml: 2 }}
            >
              Subscriptions
            </Button>
          </Box>
          {cartCount && cartCount > 0 ? (
            <Box sx={{ flexGrow: 0, mr: 4 }}>
              <Badge badgeContent={cartCount} color="primary">
                <IconButton onClick={handleCartClick} sx={{ p: 0 }}>
                  <ShoppingCartIcon />
                </IconButton>
              </Badge>
              <Popper open={openCart} anchorEl={anchorElCart} transition>
                {({ TransitionProps }) => (
                  <Fade {...TransitionProps} timeout={350}>
                    <Box>
                      <CartView />
                    </Box>
                  </Fade>
                )}
              </Popper>
            </Box>
          ) : (
            <Box sx={{ flexGrow: 0, mr: 4 }}>
              <Badge color="primary">
                <IconButton sx={{ p: 0 }}>
                  <ShoppingCartIcon />
                </IconButton>
              </Badge>
            </Box>
          )}
          <Box sx={{ marginRight: "20px" }}>Hey, {user?.email}</Box>

          <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                <Avatar src="/static/images/avatar/2.jpg" />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: "45px" }}
              id="menu-appbar"
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              <MenuItem onClick={() => dispatch(removeUser())}>
                <Typography textAlign="center">Logout</Typography>
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default UserHeader;
