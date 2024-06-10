import { FC } from "react";
import { Box } from "@mui/material";
import { Pie } from "react-chartjs-2";

import "./PieChart.css"

interface PieChartProps {
  data: {
    labels: string[];
    datasets: {
      label: string;
      data: number[];
      backgroundColor: string[];
    }[];
  };
}

export const PieChart: FC<PieChartProps> = ({ data }: PieChartProps) => {
  return (
    <Box className={"pie-chart"}>
      <Pie data={data} />
    </Box>
  );
};
