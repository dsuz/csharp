using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// break �̎g�p��Ƃ��āA�f���𔻒肷��B
/// </summary>
public class BreakExample : MonoBehaviour
{
    /// <summary>���ʂ�\������ UI</summary>
    [SerializeField] Text _text;

    /// <summary>
    /// �f���𔻒肷��B�O������Ăяo���Ďg���B
    /// </summary>
    /// <param name="inputField">������������Ƃ��ē����Ă��� UI</param>
    public void IsPrime(InputField inputField)
    {
        // ���͂��������񂪐����ɕϊ��ł��邩�`�F�b�N����
        if (int.TryParse(inputField.text, out _))
        {
            // �����ɕϊ��ł���ꍇ
            int n = int.Parse(inputField.text);
            // �f���𔻒肷��B
            IsPrime(n);
        }
        else
        {
            // ���͂��ꂽ�����񂪐����Ƃ��ĕs���������ꍇ
            _text.text = "int �͈̔͂� ��������͂��ĉ�����";
        }
    }

    /// <summary>
    /// �f���𔻒肵�Č��ʂ�\������B
    /// �P���ȁu�G���g�X�e�l�X�̂ӂ邢�v���g���đf���𔻒肵�Ă���B
    /// �^����ꂽ���� 2 �����̏ꍇ�͍l�����Ă��Ȃ��̂Œ��ӂ��邱�ƁB
    /// </summary>
    /// <param name="number">�f�����ǂ������肷��l</param>
    void IsPrime(int number)
    {
        bool isPrime = true; // �f���ł���Ɖ��肵�ď������n�߂�

        // 2, 3, 4, 5, ... �ŏ��ԂɊ���Z�����Ă���
        for (int i = 2; i < number; i++)
        {
            if (number % i == 0) // ����؂ꂽ��f���ł͂Ȃ�
            {
                _text.text = $"{number} �� {i} �Ŋ���؂�� �f���ł͂Ȃ�";
                isPrime = false;    // �f���ł͂Ȃ������m�肵��
                break;  // ����ȏ㏈�������Ă����ʂȂ̂Ń��[�v�𔲂���ibreak �̎g�p��j
            }
        }

        // �f���������烁�b�Z�[�W��\������
        if (isPrime)
        {
            _text.text = $"{number} �͑f���ł���";
        }
    }
}
