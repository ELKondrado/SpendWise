import { FC } from "react";
import { Box } from "@mui/material";
import { Bar } from "react-chartjs-2";

import "./BarChart.css"

interface BarChartProps {
  data: {
    labels: string[];
    datasets: {
      label: string;
      data: number[];
      backgroundColor: string[];
    }[];
  };
}

export const BarChart: FC<BarChartProps> = ({ data }: BarChartProps) => {
  return (
    <Box className={"bar-chart"}>
      <Bar data={data} />
    </Box>
  );
};
