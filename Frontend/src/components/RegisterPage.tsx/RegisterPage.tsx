import * as React from "react";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useNavigate } from "react-router-dom";
import { registerBuyer, registerSeller } from "../../services/auth";
import {
  RadioGroup,
  FormControlLabel,
  FormControl,
  Radio,
} from "@mui/material";
import { useDispatch } from "react-redux";
import { User, saveUser } from "../../features/AuthSlice";
import { getUserFromTokens } from "../../features/SliceHelpers";

export default function SignUp() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [formData, setFormData] = React.useState({ email: "", password: "" });
  const [userType, setUserType] = React.useState("Buyer");
  const [confirmPassword, setConfirmPassword] = React.useState("");

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const handleRegister = () => {
    switch (userType) {
      case "Buyer":
        return registerBuyer(formData);
      case "Seller":
        return registerSeller(formData);
    }
  };

  const handleSignup = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (formData.password !== confirmPassword) {
      alert("Password and Confirm Password do not match");
      return;
    }

    const tokens = await handleRegister();
    if (tokens) {
      const user: User = getUserFromTokens(
        tokens.accessToken,
        tokens.refreshToken
      );
      dispatch(saveUser({ user: user }));
      navigate("/");
    } else {
      alert("Registration failed");
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h5">
          Sign up
        </Typography>
        <Box component="form" onSubmit={handleSignup} sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            type="email"
            fullWidth
            id="email"
            label="Email Address"
            name="email"
            autoComplete="email"
            autoFocus
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="confirmPassword"
            label="Confirm Password"
            type="password"
            id="confirmPassword"
            autoComplete="confirm-password"
            onChange={(e) => setConfirmPassword(e.target.value)}
          />

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign up
          </Button>
          <FormControl>
            <RadioGroup
              aria-labelledby="demo-radio-buttons-group-label"
              defaultValue={userType}
              name="radio-buttons-group"
              onChange={(e) => setUserType(e.target.value)}
            >
              <FormControlLabel
                value="Seller"
                control={<Radio />}
                label="Seller"
              />
              <FormControlLabel
                value="Buyer"
                control={<Radio />}
                label="Buyer"
              />
            </RadioGroup>
          </FormControl>
        </Box>
      </Box>
    </Container>
  );
}
