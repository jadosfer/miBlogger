using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBlog.Models
{
    public class MantenimientoPost
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["administracion"].ToString();
            con = new SqlConnection(constr);
        }

        public int Alta(Post post)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into Posteos(id, titulo, genero, texto) values (@id, @titulo, @texto)", con);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters.Add("@titulo", SqlDbType.VarChar);
            comando.Parameters.Add("@genero", SqlDbType.VarChar);
            comando.Parameters.Add("@texto", SqlDbType.VarChar);
            
            comando.Parameters["@id"].Value = post.Id;
            comando.Parameters["@titulo"].Value = post.Titulo;
            comando.Parameters["@genero"].Value = post.Genero;
            comando.Parameters["@texto"].Value = post.Texto;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public List<Post> RecuperarTodos()
        {
            Conectar();
            List<Post> posts = new List<Post>();

            SqlCommand com = new SqlCommand("select id,titulo,genero, texto from Posteos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Post post = new Post
                {
                    Id = int.Parse(registros["id"].ToString()),
                    Titulo = registros["titulo"].ToString(),
                    Genero = registros["genero"].ToString(),
                    Texto = registros["texto"].ToString()
                };
                posts.Add(post);
            }
            con.Close();
            return posts;
        }

        public Post Recuperar(int id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("select id,titulo,genero, texto from Posteos where id=@id", con);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Post Post = new Post();
            if (registros.Read())
            {
                Post.Id = int.Parse(registros["id"].ToString());
                Post.Titulo = registros["titulo"].ToString();
                Post.Genero = registros["genero"].ToString();
                Post.Texto = registros["texto"].ToString();
            }
            con.Close();
            return Post;
        }


        public int Modificar(Post post)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("update Posteos set titulo=@titulo, genero=@genero, texto=@texto where id=@id", con);
            comando.Parameters.Add("@titulo", SqlDbType.VarChar);
            comando.Parameters["@titulo"].Value = post.Titulo;
            comando.Parameters.Add("@genero", SqlDbType.VarChar);
            comando.Parameters["@genero"].Value = post.Genero;
            comando.Parameters.Add("@texto", SqlDbType.VarChar);
            comando.Parameters["@texto"].Value = post.Texto;
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = post.Id;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int Borrar(int id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from Posteos where id=@id", con);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}