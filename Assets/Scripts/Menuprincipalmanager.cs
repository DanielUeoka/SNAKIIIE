using UnityEngine;
using UnityEngine.SceneManagement;


public class Menuprincipalmanager : MonoBehaviour
{
    //SeializeField é para deixar as variáveis privadas visiveis no inspector
    [SerializeField] private string NomeDaScene; //variável Sring com o nome do cenário
    [SerializeField] private GameObject PainelMenuPrincipal; //Variavel do Objeto de cena do menu principal
    [SerializeField] private GameObject PainelOpcoes; //Variavel do Objeto de cena das opções
    [SerializeField] private GameObject PainelCreditos; //Variavel do Objeto de cena dos créditos
    [SerializeField] private GameObject Mapagameplay; //Variavel do Objeto de cena da gameplay
    [SerializeField] private GameObject Painelpause; //Variavel do Objeto de cena do pause
    [SerializeField] private GameObject PainelOpcoesPause; //Variavel do Objeto de cena do pause
    public GameObject PainelGameOver;

    public void Jogar()
    {
        SceneManager.LoadScene(NomeDaScene);
    }


    public void Desistir() //Função do botão de desistir(Fechar a cena de gameplay e abrir a cena de menu principal)
    {
        Mapagameplay.SetActive(false);
        PainelMenuPrincipal.SetActive(true);
    }
    public void abriropcoes() //Função do botão de abrir as opções (Fechar a cena de menu principal e abrir a cena de opções)
    {
        PainelMenuPrincipal.SetActive(false);
        PainelOpcoes.SetActive(true);
    }

    public void fecharopcoes() //Função do bottão de fechar as opções (Fechar a cena de opções e abrir a cena de menu principal)
    {
        PainelOpcoes.SetActive(false);
        PainelMenuPrincipal.SetActive(true);
    }

    public void abrircreditos() //Função do botão de abrir os créditos (Fechar a cena de menu principal e abrir a cena de créditos)

    {
        PainelMenuPrincipal.SetActive(false);
        PainelCreditos.SetActive(true);
    }

    public void fecharcreditos() //Função do botão de fechar os créditos (Fechar a cena de creditos e abrir a cena de menu principal)
    {
        PainelCreditos.SetActive(false);
        PainelMenuPrincipal.SetActive(true);
    }

    public void abrirpause() //Função do botão de abrir o painel de pause
    {
        Painelpause.SetActive(true);
        Time.timeScale = 0;
    }

    public void fecharpause() //Função do botão de fechar o painel de pause (Resume)
    {
        Painelpause.SetActive(false);
        Time.timeScale = 1;
    }

    public void VoltarMainMenu() //Função do botão de voltar para o menu principal do menu de pause
    {
        Painelpause.SetActive(false);
        Mapagameplay.SetActive(false);
        PainelGameOver.SetActive(false);
        PainelMenuPrincipal.SetActive(true);
    }

    public void AbrirOpcoesPause() //Função do botão de abrir o menu de opções do menu de pause
    {
        PainelOpcoesPause.SetActive(true);
    }

    public void FecharOpcoesPause() //Função do botão de fechar o menu de opções do menu de pause
    {
        PainelOpcoesPause.SetActive(false);
    }

    public void Sairdojogo()
    {
        Debug.Log("sair do jogo"); //mostrar mensagem de feedback no console
        Application.Quit(); //fechar o jogo
    }
}
