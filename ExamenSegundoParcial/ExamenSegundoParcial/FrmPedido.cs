using Datos.Accesos;
using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenSegundoParcial
{
    public partial class FrmPedido : Form
    {
        public FrmPedido()
        {
            InitializeComponent();
        }
        ProductoDA productoDA = new ProductoDA();
        Pedido pedido= new Pedido();
        Producto producto;

        List<DetallePedido> detallePedidoLista = new List<DetallePedido>();

        decimal subTotal = decimal.Zero;
        decimal isv = decimal.Zero;
        decimal totalAPagar = decimal.Zero;

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            DetalleDataGridView.DataSource = detallePedidoLista;
        }

        private void CodigoProductoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                producto = new Producto();
                producto = productoDA.GetProductoPorCodigo(CodigoProductoTextBox.Text);
                DescripcionProductoTextBox.Text = producto.Descripcion;
                CantidadTextBox.Focus();
            }
            else
            {
                producto = null;
                DescripcionProductoTextBox.Clear();
                CantidadTextBox.Clear();
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                DetallePedido detallepedido = new DetallePedido();
                detallepedido.CodigoProducto = producto.Codigo;
                detallepedido.Descripcion = producto.Descripcion;
                detallepedido.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                detallepedido.Precio = producto.Precio;
                detallepedido.Total = producto.Precio * Convert.ToInt32(CantidadTextBox.Text);

                subTotal += detallepedido.Total;
                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv;


                detallePedidoLista.Add(detallepedido);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = detallePedidoLista;


            }
        }
    }
}
